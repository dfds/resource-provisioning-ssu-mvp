using System;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public class ResourceProvisioningCompletedEvent : IntegrationEvent, IProvisioningEvent
	{
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningCompletedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
