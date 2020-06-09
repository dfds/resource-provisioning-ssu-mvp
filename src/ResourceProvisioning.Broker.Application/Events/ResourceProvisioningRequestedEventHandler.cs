using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class ResourceProvisioningRequestedEventHandler : IPivotEventHandler<ResourceProvisioningRequestedEvent>
	{	
		public Task Handle(ResourceProvisioningRequestedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
