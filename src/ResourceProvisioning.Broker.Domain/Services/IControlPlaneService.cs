using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Services;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public interface IControlPlaneService : IService
	{
		Task<IEnumerable<IAggregateRoot>> GetAggregatesByState(IDesiredState desiredState);
		
		Task<IEnumerable<EnvironmentRoot>> GetEnvironmentsAsync();

		Task<EnvironmentRoot> GetEnvironmentByIdAsync(Guid environmentId);

		Task<IEnumerable<EnvironmentRoot>> GetEnvironmentByResourceIdAsync(Guid resourceId);

		Task<EnvironmentRoot> AddEnvironmentAsync(IDesiredState desiredState, CancellationToken cancellationToken = default);

		Task<EnvironmentRoot> UpdateEnvironmentAsync(Guid environmentId, IDesiredState desiredState, CancellationToken cancellationToken = default);

		Task DeleteEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default);

		Task<ResourceRoot> GetResourceByIdAsync(Guid resourceId);

		Task<ResourceRoot> AddResourceAsync(IDesiredState desiredState, CancellationToken cancellationToken = default);

		Task<ResourceRoot> UpdateResourceAsync(Guid resourceId, IDesiredState desiredState, CancellationToken cancellationToken = default);

		Task DeleteResourceAsync(Guid resourceId, CancellationToken cancellationToken = default);
	}
}
