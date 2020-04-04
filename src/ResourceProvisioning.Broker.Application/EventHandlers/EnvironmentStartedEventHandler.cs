using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

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
