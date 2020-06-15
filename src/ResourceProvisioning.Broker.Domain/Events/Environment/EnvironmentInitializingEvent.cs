using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
{
	public sealed class EnvironmentInitializingEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentInitializingEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
