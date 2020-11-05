using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
{
	public sealed class EnvironmentCreatedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentCreatedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
