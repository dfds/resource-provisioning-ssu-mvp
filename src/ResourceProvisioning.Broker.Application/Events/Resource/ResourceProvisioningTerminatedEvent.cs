using System;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public class ResourceProvisioningTerminatedEvent : IntegrationEvent, IProvisioningEvent
	{
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningTerminatedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
