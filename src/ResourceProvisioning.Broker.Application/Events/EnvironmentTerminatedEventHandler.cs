using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentTerminatedEventHandler : IDomainEventHandler<EnvironmentStoppedEvent>
	{
		public async Task Handle(EnvironmentStoppedEvent environmentCancelledEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
