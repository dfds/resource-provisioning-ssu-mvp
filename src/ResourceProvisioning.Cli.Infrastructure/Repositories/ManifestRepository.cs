using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Cli.Domain.Repositories;

namespace ResourceProvisioning.Cli.Infrastructure.Repositories
{
	public class ManifestRepository<T> : IManifestRepository<T> where T : class, IDesiredState
	{
		private readonly JsonSerializerOptions _serializerOptions;
		private readonly DirectoryInfo _rootDirectory;

		public IUnitOfWork UnitOfWork => throw new NotImplementedException();

		public ManifestRepository(DirectoryInfo rootDirectory = default, JsonSerializerOptions serializerOptions = default)
		{
			_rootDirectory = rootDirectory;
			_serializerOptions = serializerOptions;
		}

		public Task<IEnumerable<T>> GetDesiredStatesByIdAsync(Guid environmentId)
		{
			var files = _rootDirectory.GetFiles($"*{environmentId.ToString()}*");
			var desiredStates =
				files.Select(file =>
					JsonSerializer.Deserialize<T>(
						File.ReadAllText(file.FullName), _serializerOptions)
					);

			return Task.FromResult(desiredStates);
		}

		public Task StoreDesiredStateAsync(Guid environmentId, T desiredState)
		{
			var fileName = $"{environmentId.ToString()}.json";
			var filePath = Path.Combine(_rootDirectory.FullName, fileName);

			if (!File.Exists(filePath))
			{
				File.WriteAllText(filePath, JsonSerializer.Serialize(desiredState, typeof(T)));
			}

			return Task.CompletedTask;
		}
	}
}
