using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextStatusChangedToApprovedDomainEvent : IDomainEvent
	{
		public Guid ContextId { get; }

		public ContextStatusChangedToApprovedDomainEvent(Guid contextId)
		{
			ContextId = contextId;
		}
	}
}
