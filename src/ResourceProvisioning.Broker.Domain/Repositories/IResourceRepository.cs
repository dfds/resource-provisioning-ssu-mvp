using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;

namespace ResourceProvisioning.Broker.Domain.Repository
{
	public interface IResourceRepository : IRepository<ResourceRoot>
	{
		Task<ResourceRoot> GetByIdAsync(Guid resourceId);
	}
}
