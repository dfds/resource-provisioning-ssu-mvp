﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentCreatedEventHandler : IDomainEventHandler<EnvironmentCreatedEvent>
	{
		public async Task HandleAsync(EnvironmentCreatedEvent environmentCreatedEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
