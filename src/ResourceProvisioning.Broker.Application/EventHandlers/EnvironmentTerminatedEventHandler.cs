using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentTerminatedEventHandler : IDomainEventHandler<EnvironmentTerminatedEvent>
	{
		public async Task Handle(EnvironmentTerminatedEvent environmentCancelledEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
