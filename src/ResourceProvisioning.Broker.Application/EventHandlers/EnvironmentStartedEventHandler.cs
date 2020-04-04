using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.Repository;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentStartedEventHandler : IDomainEventHandler<EnvironmentStartedEvent>
	{
		public async Task Handle(EnvironmentStartedEvent environmentCompletedEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
