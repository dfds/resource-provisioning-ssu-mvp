using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Environment
{
	public sealed class EnvironmentCreatedEventHandler : IDomainEventHandler<EnvironmentCreatedEvent>
	{
		public Task Handle(EnvironmentCreatedEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
