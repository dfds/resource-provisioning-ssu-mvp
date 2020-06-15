using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public class ResourceInitializingEventHandler : IDomainEventHandler<ResourceInitializingEvent>
	{
		public async Task Handle(ResourceInitializingEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
