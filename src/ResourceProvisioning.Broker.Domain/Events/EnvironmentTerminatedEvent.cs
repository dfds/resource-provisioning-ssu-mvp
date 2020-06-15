using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentTerminatedEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentTerminatedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
