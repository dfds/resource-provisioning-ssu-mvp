using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public interface IDomainService
	{
		Task<Aggregates.EnvironmentAggregate.Environment> GetEnvironmentByIdAsync(Guid environmentId);

		Task<IEnumerable<Aggregates.EnvironmentAggregate.Environment>> GetEnvironmentByResourceIdAsync(Guid resourceId);

		Task<Aggregates.EnvironmentAggregate.Environment> AddEnvironmentAsync(DesiredState desiredState, CancellationToken cancellationToken = default);

		Task<Aggregates.EnvironmentAggregate.Environment> UpdateEnvironmentAsync(Guid environmentId, DesiredState desiredState, CancellationToken cancellationToken = default);

		Task DeleteEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default);
	}
}
