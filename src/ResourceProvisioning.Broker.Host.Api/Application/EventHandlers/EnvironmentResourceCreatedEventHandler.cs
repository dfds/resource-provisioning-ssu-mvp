using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ResourceProvisioning.Abstractions.Events.Integration;
using ResourceProvisioning.Broker.Host.Api.Application.IntegrationEvents.Events;

namespace ResourceProvisioning.Broker.Host.Api.Application.EventHandlers
{
	public class EnvironmentResourceCreatedEventHandler : IIntegrationEventHandler<EnvironmentResourceCreatedEvent>
	{	
		public Task HandleAsync(EnvironmentResourceCreatedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
