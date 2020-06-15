using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Repositories;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Repositories;

namespace ResourceProvisioning.Cli.Application.Commands
{
	[Command(Description = "Applies desired state to the given environment")]
    public sealed class Apply : CliCommand
    {
        private readonly IBrokerService _broker;
        private readonly IManifestRepository<DesiredState> _manifestRepository;

		[Argument(0)]
        public string DesiredStateSource {
            get;
            set;
        }

        public Apply(IBrokerService broker, IManifestRepository<DesiredState> manifestRepository)
        {
	        _broker = broker;
	        _manifestRepository = manifestRepository;
        }

        public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var desiredState in await GetDesiredStateData())
                {
                    Console.WriteLine($"Posting desiredState {JsonSerializer.Serialize(desiredState)} to broker");

                    await _broker.ApplyDesiredStateAsync(Guid.Parse(EnvironmentId), desiredState, cancellationToken);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return -1;
            }

            return 0;
        }

        private async Task<IEnumerable<DesiredState>> GetDesiredStateData()
        {
            if (IsValidJson(DesiredStateSource))
            {
                return new[] { JsonSerializer.Deserialize<DesiredState>(DesiredStateSource) };
            }

            if (!Directory.Exists(DesiredStateSource))
            {
	            return File.Exists(DesiredStateSource)
		            ? new[] {JsonSerializer.Deserialize<DesiredState>(File.ReadAllText(DesiredStateSource))}
		            : null;
            }

            _manifestRepository.RootDirectory = DesiredStateSource;

            return await _manifestRepository.GetDesiredStatesByIdAsync(Guid.Parse(EnvironmentId));
        }

        private static bool IsValidJson(string json) 
        {
            try
            {
                JsonDocument.Parse(json);
            }
            catch(JsonException)
            {
                return false;
            }

            return true;
        }
    }
}
