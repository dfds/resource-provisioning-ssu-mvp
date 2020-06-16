using System;
using System.Text.Json;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Events.Provisioning
{
	public sealed class ProvisioningRequestedEvent : IntegrationEvent, IProvisioningEvent
	{
		public ProvisioningRequestedEvent(JsonElement payload, Guid correlationId = default) : base(nameof(ProvisioningRequestedEvent), payload, correlationId: correlationId)
		{
		}
	}
}
