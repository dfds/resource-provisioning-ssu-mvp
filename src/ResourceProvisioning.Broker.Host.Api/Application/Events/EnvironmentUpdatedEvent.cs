using System;
using Newtonsoft.Json;
using ResourceProvisioning.Abstractions.Events.Integration;

namespace ResourceProvisioning.Broker.Host.Api.Application.IntegrationEvents.Events
{
	public class EnvironmentUpdatedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid EnvironmentId { get; private set; }

		public EnvironmentUpdatedEvent(Guid environmentId) => EnvironmentId = environmentId;
	}
}
