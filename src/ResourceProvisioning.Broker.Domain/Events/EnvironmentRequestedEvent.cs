using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public sealed class EnvironmentRequestedEvent : IPivotEvent
	{
		public EnvironmentRoot Environment { get; }

		public EnvironmentRequestedEvent(EnvironmentRoot environment)
		{
			Environment = environment;
		}
	}
}
