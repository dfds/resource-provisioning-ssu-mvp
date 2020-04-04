using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentChangedEvent : BaseIntegrationEvent
	{
		public Guid EnvironmentId { get; private set; }

		public EnvironmentChangedEvent(Guid environmentId) => EnvironmentId = environmentId;
	}
}
