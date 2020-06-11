using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.Core.Core.Repositories;

namespace ResourceProvisioning.Cli.Application.Repositories
{
    public class ManifestRepository : IDesiredStateRepository, IStateRepository
    {
        private readonly string _rootDirectory;
        private readonly JsonSerializerOptions _serializerOptions;

        public ManifestRepository(string manifestDirectory, JsonSerializerOptions serializerOptions = default) 
        {
            _rootDirectory = manifestDirectory;
            _serializerOptions = serializerOptions;
        }

        public Task<IEnumerable<ActualState>> GetStatesByIdAsync(Guid environmentId)
        {
            var files = Directory.GetFiles(_rootDirectory, $"*{environmentId.ToString()}*");
            var desiredStates =
                files.Select(path => 
                    JsonSerializer.Deserialize<ActualState>(
                        File.ReadAllText(path), _serializerOptions)
                    );

            return Task.FromResult(desiredStates);
        }

        public Task StoreDesiredStateAsync(Guid environmentId, DesiredState desiredState)
        {
            var fileName = $"{environmentId.ToString()}.json";
            var filePath = Path.Combine(_rootDirectory, fileName);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonSerializer.Serialize(desiredState, typeof(DesiredState)));
            }

            return Task.CompletedTask;
        }
    }
}
