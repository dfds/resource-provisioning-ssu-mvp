using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Environment
{
	public sealed class EnvironmentRequestedEventHandler : IDomainEventHandler<EnvironmentRequestedEvent>
	{
		public Task Handle(EnvironmentRequestedEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
