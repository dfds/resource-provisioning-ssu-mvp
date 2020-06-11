using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Repositories;

namespace ResourceProvisioning.Cli.Infrastructure.Repositories
{
	public interface IManifestRepository<T> : IRepository where T : class, IDesiredState
	{
        Task StoreDesiredStateAsync(
            Guid environmentId,
            IDesiredState desiredState
        );

		Task<IEnumerable<T>> GetStatesByIdAsync(
			Guid environmentId
		);
	}
}
