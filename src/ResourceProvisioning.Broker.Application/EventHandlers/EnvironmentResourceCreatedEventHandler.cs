using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Application.Events;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentResourceCreatedEventHandler : IIntegrationEventHandler<ResourceProvisioningCompletedEvent>
	{	
		public Task HandleAsync(ResourceProvisioningCompletedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
