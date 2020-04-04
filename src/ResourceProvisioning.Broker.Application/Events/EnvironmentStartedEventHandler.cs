using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentStartedEventHandler : IDomainEventHandler<EnvironmentStartedEvent>
	{
		public async Task Handle(EnvironmentStartedEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
