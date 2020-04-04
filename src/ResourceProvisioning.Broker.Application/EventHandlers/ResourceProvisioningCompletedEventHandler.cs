using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Application.Events;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class ResourceProvisioningCompletedEventHandler : IIntegrationEventHandler<ResourceProvisioningCompletedEvent>
	{	
		public Task Handle(ResourceProvisioningCompletedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
