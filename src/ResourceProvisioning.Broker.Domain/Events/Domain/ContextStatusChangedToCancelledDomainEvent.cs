using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextStatusChangedToCancelledDomainEvent : IDomainEvent
	{
		public Guid ContextId { get; }

		public ContextStatusChangedToCancelledDomainEvent(Guid contextId)
		{
			ContextId = contextId;
		}
	}
}
