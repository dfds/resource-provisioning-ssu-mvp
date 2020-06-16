using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceUnavailableEventHandler : IEventHandler<ResourceUnavailableEvent>
	{
		public Task Handle(ResourceUnavailableEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
