using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentInitializingEventHandler : IDomainEventHandler<EnvironmentInitializingEvent>
	{
		public async Task Handle(EnvironmentInitializingEvent environmentProvisioningEvent, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
