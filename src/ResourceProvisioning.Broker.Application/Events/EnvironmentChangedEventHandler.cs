using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class EnvironmentChangedEventHandler : IIntegrationEventHandler<EnvironmentChangedEvent>
	{	
		public Task Handle(EnvironmentChangedEvent @event, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
