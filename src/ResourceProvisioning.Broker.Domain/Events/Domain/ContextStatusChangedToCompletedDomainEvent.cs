using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextStatusChangedToCompletedDomainEvent : IDomainEvent
	{
		public Guid ContextId { get; }

		public ContextStatusChangedToCompletedDomainEvent(Guid contextId)
		{
			ContextId = contextId;
		}
	}
}
