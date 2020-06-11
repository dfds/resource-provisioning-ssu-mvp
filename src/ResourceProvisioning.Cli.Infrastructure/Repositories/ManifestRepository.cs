using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Cli.Infrastructure.Repositories;

namespace ResourceProvisioning.Cli.Application.Repositories
{
	public class ManifestRepository<T> : IManifestRepository<T> where T : class, IDesiredState
	{
        private readonly string _rootDirectory;
        private readonly JsonSerializerOptions _serializerOptions;

		//TODO: Down the line we should introduce a unit of work concept for this. It could be done using virtual memory mapped files which stay in memory until save changes is called.
		public IUnitOfWork UnitOfWork => null;

		public ManifestRepository(string manifestDirectory, JsonSerializerOptions serializerOptions = default) 
        {
            _rootDirectory = manifestDirectory;
            _serializerOptions = serializerOptions;
        }

		public Task<IEnumerable<T>> GetDesiredStatesByIdAsync(Guid environmentId)
        {
            var files = Directory.GetFiles(_rootDirectory, $"*{environmentId.ToString()}*");
            var desiredStates =
                files.Select(path => 
                    JsonSerializer.Deserialize<T>(
                        File.ReadAllText(path), _serializerOptions)
                    );

            return Task.FromResult(desiredStates);
        }

        public Task StoreDesiredStateAsync(Guid environmentId, T desiredState)
        {
            var fileName = $"{environmentId.ToString()}.json";
            var filePath = Path.Combine(_rootDirectory, fileName);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonSerializer.Serialize(desiredState, typeof(T)));
            }

            return Task.CompletedTask;
        }
	}
}
