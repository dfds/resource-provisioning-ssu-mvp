using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentStatusChangedToTerminatedEventHandler : IDomainEventHandler<EnvironmentStatusChangedToTerminatedEvent>
	{
		public async Task HandleAsync(EnvironmentStatusChangedToTerminatedEvent environmentCancelledEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
