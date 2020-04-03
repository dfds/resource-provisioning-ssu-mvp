using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentStatusChangedToReadyEventHandler : IDomainEventHandler<EnvironmentStatusChangedToReadyEvent>
	{
		public async Task HandleAsync(EnvironmentStatusChangedToReadyEvent environmentCompletedEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
