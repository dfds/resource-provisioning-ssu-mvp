using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Repositories;

namespace ResourceProvisioning.Broker.Repository
{
	public interface IEnvironmentRepository : IRepository<Domain.Aggregates.EnvironmentAggregate.Environment>
	{
		Task<Domain.Aggregates.EnvironmentAggregate.Environment> GetByIdAsync(Guid environmentId);
	}
}
