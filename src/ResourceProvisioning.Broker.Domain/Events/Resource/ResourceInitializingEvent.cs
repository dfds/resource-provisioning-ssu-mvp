using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
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
