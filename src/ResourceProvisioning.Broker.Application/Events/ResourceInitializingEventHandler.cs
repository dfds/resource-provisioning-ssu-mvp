using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class ResourceInitializingEventHandler : IDomainEventHandler<ResourceUnavailableEvent>
	{
		public async Task Handle(ResourceUnavailableEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
