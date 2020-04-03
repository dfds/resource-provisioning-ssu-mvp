using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToCompletedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToCompletedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
