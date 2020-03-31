using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Service
{
	public interface IDesiredStateService
	{
		Task<DesiredState> GetByContextId(Guid contextId);
	}
}
