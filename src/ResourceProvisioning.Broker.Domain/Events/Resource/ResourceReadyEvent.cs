using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
{
	public sealed class ResourceReadyEvent : IDomainEvent
	{
		public Guid ResourceId { get; }

		public ResourceReadyEvent(Guid resourceId)
		{
			ResourceId = resourceId;
		}
	}
}
