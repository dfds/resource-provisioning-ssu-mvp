using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToCancelledEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToCancelledEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
