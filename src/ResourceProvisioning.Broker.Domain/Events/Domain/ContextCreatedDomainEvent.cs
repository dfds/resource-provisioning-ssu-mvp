using ResourceProvisioning.Abstractions.Events.Domain;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextCreatedDomainEvent : IDomainEvent
	{
		public Context Context { get; }

		public ContextCreatedDomainEvent(Context context)
		{
			Context = context;
		}
	}
}
