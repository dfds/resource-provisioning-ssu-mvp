using System;
using Newtonsoft.Json;
using ResourceProvisioning.Abstractions.Events.Integration;

namespace ResourceProvisioning.Broker.Application.IntegrationEvents.Events
{
	//TODO: Fake this event as it will be published by IProvisioningProviders
	public class ResourceProvisioningCompletedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningCompletedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
