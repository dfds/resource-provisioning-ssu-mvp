using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;

namespace ResourceProvisioning.Broker.Domain.Events.Environment
{
	public sealed class EnvironmentRequestedEvent : IDomainEvent
	{
		public EnvironmentRoot Environment { get; }

		public EnvironmentRequestedEvent(EnvironmentRoot environment)
		{
			Environment = environment;
		}
	}
}
