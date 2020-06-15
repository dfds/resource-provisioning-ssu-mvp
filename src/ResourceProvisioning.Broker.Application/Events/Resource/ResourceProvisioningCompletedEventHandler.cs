using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events.Resource
{
	public sealed class ResourceProvisioningCompletedEventHandler : IIntegrationEventHandler<ResourceProvisioningCompletedEvent>
	{
		public Task Handle(ResourceProvisioningCompletedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
