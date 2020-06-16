using System;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceProvisioningRequestedEvent : IntegrationEvent, IProvisioningEvent
	{
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningRequestedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
