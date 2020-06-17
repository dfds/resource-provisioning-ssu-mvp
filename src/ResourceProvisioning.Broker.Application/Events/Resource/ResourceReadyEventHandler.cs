using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceReadyEventHandler : IEventHandler<ResourceReadyEvent>
	{
		public Task Handle(ResourceReadyEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
