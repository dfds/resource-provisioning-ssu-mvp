using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;

namespace ResourceProvisioning.Broker.Domain.Repository
{
	public interface IEnvironmentRepository : IRepository<EnvironmentRoot>
	{
		Task<EnvironmentRoot> GetByIdAsync(Guid environmentId);
	}
}
