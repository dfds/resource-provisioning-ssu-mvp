using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Environment
{
	public sealed class EnvironmentTerminatedEventHandler : IDomainEventHandler<EnvironmentTerminatedEvent>
	{
		public async Task Handle(EnvironmentTerminatedEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
