using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events
{
	public class EnvironmentStatusChangedToProvisioningEvent : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToProvisioningEvent(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
