using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToInitializingEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToInitializingEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
