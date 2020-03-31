using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Repository
{
	public interface IContextRepository : IRepository<Context>
	{
		Task<Context> GetByIdAsync(Guid ContextId);
	}
}
