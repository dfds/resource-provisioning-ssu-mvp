using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Domain.Repository
{
	public interface IEnvironmentRepository : IRepository<EnvironmentRoot>
	{
		Task<EnvironmentRoot> GetByIdAsync(Guid environmentId);
	}
}
