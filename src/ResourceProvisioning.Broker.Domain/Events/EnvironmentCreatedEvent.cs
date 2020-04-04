using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentCreatedEvent : IDomainEvent
	{
		public EnvironmentRoot Environment { get; }

		public EnvironmentCreatedEvent(EnvironmentRoot environment)
		{
			Environment = environment;
		}
	}
}
