using System;
using Newtonsoft.Json;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	//TODO: Fake this event as it will be published by IProvisioningProviders
	public class ResourceProvisioningCompletedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningCompletedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
