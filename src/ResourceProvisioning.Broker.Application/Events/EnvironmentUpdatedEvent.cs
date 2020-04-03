using System;
using Newtonsoft.Json;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentUpdatedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid EnvironmentId { get; private set; }

		public EnvironmentUpdatedEvent(Guid environmentId) => EnvironmentId = environmentId;
	}
}
