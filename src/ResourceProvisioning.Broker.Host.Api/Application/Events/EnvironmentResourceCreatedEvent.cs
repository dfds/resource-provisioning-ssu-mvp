using System;
using Newtonsoft.Json;
using ResourceProvisioning.Abstractions.Events.Integration;

namespace ResourceProvisioning.Broker.Host.Api.Application.IntegrationEvents.Events
{
	public class EnvironmentResourceCreatedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid ResourceId { get; private set; }

		public EnvironmentResourceCreatedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
