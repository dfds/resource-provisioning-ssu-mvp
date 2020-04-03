using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events.Integration;
using ResourceProvisioning.Broker.Application.IntegrationEvents.Events;

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
