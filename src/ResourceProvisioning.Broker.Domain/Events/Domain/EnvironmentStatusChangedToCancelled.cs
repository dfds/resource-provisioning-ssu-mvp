using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events.Domain
{
	public class EnvironmentStatusChangedToCancelled : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToCancelled(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
