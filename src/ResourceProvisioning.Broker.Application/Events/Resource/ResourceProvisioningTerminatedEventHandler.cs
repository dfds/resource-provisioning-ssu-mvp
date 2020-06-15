﻿using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceProvisioningTerminatedEventHandler : IIntegrationEventHandler<ResourceProvisioningTerminatedEvent>
	{
		public Task Handle(ResourceProvisioningTerminatedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
