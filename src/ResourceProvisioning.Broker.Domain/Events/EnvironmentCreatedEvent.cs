using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentCreatedEvent : IPivotEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentCreatedEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
