using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.Repository;

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
