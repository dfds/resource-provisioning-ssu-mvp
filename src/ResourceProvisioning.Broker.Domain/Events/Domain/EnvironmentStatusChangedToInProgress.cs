using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events.Domain
{
	public class EnvironmentStatusChangedToInProgress : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentStatusChangedToInProgress(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
