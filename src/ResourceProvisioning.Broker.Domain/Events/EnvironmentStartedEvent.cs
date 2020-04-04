using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentStartedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStartedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
