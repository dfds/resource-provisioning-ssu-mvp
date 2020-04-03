using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events.Domain
{
	public class EnvironmentCreated : IDomainEvent
	{
		public Aggregates.EnvironmentAggregate.Environment Environment { get; }

		public EnvironmentCreated(Aggregates.EnvironmentAggregate.Environment environment)
		{
			Environment = environment;
		}
	}
}
