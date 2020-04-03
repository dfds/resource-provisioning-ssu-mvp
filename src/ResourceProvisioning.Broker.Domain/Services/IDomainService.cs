using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Service
{
	public interface IDomainService
	{
		Task<Domain.Aggregates.EnvironmentAggregate.Environment> GetEnvironmentByIdAsync(Guid environmentId);

		Task<Domain.Aggregates.EnvironmentAggregate.Environment> AddEnvironmentAsync(DesiredState state);

		Task<Domain.Aggregates.EnvironmentAggregate.Environment> UpdateEnvironmentAsync(Guid environmentId, DesiredState state);

		Task DeleteEnvironmentAsync(Guid environmentId);
	}
}
