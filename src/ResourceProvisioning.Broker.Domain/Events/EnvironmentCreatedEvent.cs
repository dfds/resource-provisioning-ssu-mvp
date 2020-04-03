using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentCreatedEvent : IDomainEvent
	{
		public Aggregates.EnvironmentAggregate.Environment Environment { get; }

		public EnvironmentCreatedEvent(Aggregates.EnvironmentAggregate.Environment environment)
		{
			Environment = environment;
		}
	}
}
