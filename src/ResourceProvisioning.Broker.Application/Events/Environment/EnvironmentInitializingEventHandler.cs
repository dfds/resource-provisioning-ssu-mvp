using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events.Environment;

namespace ResourceProvisioning.Broker.Application.Events.Environment
{
	public sealed class EnvironmentInitializingEventHandler : IDomainEventHandler<EnvironmentInitializingEvent>
	{
		public Task Handle(EnvironmentInitializingEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
