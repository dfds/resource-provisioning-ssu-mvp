using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentStoppedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStoppedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
