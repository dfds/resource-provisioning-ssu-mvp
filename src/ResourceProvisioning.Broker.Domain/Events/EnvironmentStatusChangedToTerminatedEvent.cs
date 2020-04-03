using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToTerminatedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToTerminatedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
