using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public interface IControlPlaneService
	{
		Task<IEnumerable<IAggregateRoot>> GetAggregatesByState(DesiredState desiredState);

		Task<EnvironmentRoot> GetEnvironmentByIdAsync(Guid environmentId);

		Task<IEnumerable<EnvironmentRoot>> GetEnvironmentByResourceIdAsync(Guid resourceId);

		Task<EnvironmentRoot> AddEnvironmentAsync(DesiredState desiredState, CancellationToken cancellationToken = default);

		Task<EnvironmentRoot> UpdateEnvironmentAsync(Guid environmentId, DesiredState desiredState, CancellationToken cancellationToken = default);

		Task DeleteEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default);

		Task<ResourceRoot> GetResourceByIdAsync(Guid resourceId);

		Task<ResourceRoot> AddResourceAsync(DesiredState desiredState, CancellationToken cancellationToken = default);

		Task<ResourceRoot> UpdateResourceAsync(Guid resourceId, DesiredState desiredState, CancellationToken cancellationToken = default);

		Task DeleteResourceAsync(Guid resourceId, CancellationToken cancellationToken = default);
	}
}
