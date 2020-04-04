using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.Repository;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentInitializingEventHandler : IDomainEventHandler<EnvironmentInitializingEvent>
	{
		public async Task Handle(EnvironmentInitializingEvent environmentProvisioningEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
