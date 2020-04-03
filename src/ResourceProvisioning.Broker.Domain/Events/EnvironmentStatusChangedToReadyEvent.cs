using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToReadyEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToReadyEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
