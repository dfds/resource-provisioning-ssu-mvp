using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Events.Domain
{
	public class ContextStatusChangedToInProgressDomainEvent : IDomainEvent
	{
		public Guid ContextId { get; }

		public ContextStatusChangedToInProgressDomainEvent(Guid contextId)
		{
			ContextId = contextId;
		}
	}
}
