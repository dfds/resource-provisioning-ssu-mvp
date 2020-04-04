using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class ResourceInitializingEvent : IDomainEvent
	{
		public ResourceRoot Resource { get; }

		public ResourceInitializingEvent(ResourceRoot resource)
		{
			Resource = resource;
		}
	}
}
