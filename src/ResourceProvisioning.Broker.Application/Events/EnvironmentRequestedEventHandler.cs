using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentRequestedEventHandler : IDomainEventHandler<EnvironmentRequestedEvent>
	{
		public async Task Handle(EnvironmentRequestedEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
