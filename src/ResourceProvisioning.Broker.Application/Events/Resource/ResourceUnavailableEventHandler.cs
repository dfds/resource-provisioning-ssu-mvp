using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceUnavailableEventHandler : IDomainEventHandler<ResourceUnavailableEvent>
	{
		public async Task Handle(ResourceUnavailableEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
