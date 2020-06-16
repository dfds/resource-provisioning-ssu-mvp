using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Repositories;

namespace ResourceProvisioning.Cli.Domain.Repositories
{
	public interface IManifestRepository<T> : IRepository where T : class, IDesiredState
	{
		string RootDirectory { get; set; }

		Task StoreDesiredStateAsync(
			Guid environmentId,
			T desiredState
		);

		Task<IEnumerable<T>> GetDesiredStatesByIdAsync(
			Guid environmentId
		);
	}
}
