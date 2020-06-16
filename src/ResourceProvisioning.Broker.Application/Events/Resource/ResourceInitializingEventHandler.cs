using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceInitializingEventHandler : IDomainEventHandler<ResourceInitializingEvent>
	{
		public Task Handle(ResourceInitializingEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
