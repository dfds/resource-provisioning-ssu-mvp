using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextStatusChangedToAwaitingApprovalDomainEvent : IDomainEvent
	{
		public Guid ContextId { get; }

		public ContextStatusChangedToAwaitingApprovalDomainEvent(Guid contextId)
		{
			ContextId = contextId;
		}
	}
}
