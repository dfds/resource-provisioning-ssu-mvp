using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Datadog.Trace;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Repositories;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.RestClient.Core;

namespace ResourceProvisioning.Cli.Application.Commands
{
    [Command(Description = "Applies desired state to the given environment")]
    public sealed class Apply : CliCommand
    {
        private readonly IRestClient _client;

        [Argument(0)]
        public string DesiredStateSource {
            get;
            set;
        }

        public Apply(IRestClient client)
        {
            _client = client;
        }

        public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using (var scope = Tracer.Instance.StartActive("web.request"))
                {
                    var span = scope.Span;
                    span.Type = SpanTypes.Web;
                    span.ResourceName = "Apply.OnExecuteAsync.SubmitDesiredStateAsync";
                    span.SetTag(Tags.HttpMethod, "POST");

                    foreach (var desiredState in await GetDesiredStateData())
                    {
                        Console.WriteLine($"Posting desiredState {JsonSerializer.Serialize(desiredState)} to broker");

                        await _client.State.SubmitDesiredStateAsync(Guid.Parse(EnvironmentId), desiredState);
                    }                    
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
                return new DesiredState[] { JsonSerializer.Deserialize<DesiredState>(DesiredStateSource) };
            }
            else if (Directory.Exists(DesiredStateSource))
            {
                return await new ManifestRepository(DesiredStateSource).GetStatesByIdAsync(Guid.Parse(EnvironmentId));
            }
            else if (File.Exists(DesiredStateSource))
            {
                return new DesiredState[] { JsonSerializer.Deserialize<DesiredState>(File.ReadAllText(DesiredStateSource)) };
            }

            return null;
        }

        private bool IsValidJson(string json) 
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
