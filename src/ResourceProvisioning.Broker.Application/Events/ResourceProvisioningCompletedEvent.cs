using System;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class ResourceProvisioningCompletedEvent : BaseIntegrationEvent, IProvisioningEvent
	{
		public Guid ResourceId { get; private set; }

		public ResourceProvisioningCompletedEvent(Guid resourceId) => ResourceId = resourceId;
	}
}
