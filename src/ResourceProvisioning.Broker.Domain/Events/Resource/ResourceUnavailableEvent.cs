using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
{
	public sealed class ResourceUnavailableEvent : IDomainEvent
	{
		public Guid ResourceId { get; }

		public ResourceUnavailableEvent(Guid resourceId)
		{
			ResourceId = resourceId;
		}
	}
}
