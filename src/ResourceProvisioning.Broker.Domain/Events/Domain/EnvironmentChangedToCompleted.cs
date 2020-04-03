using System;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Broker.Domain.Events.Domain
{
	public class EnvironmentChangedToCompleted : IDomainEvent
	{
		public Guid EnvironmentId { get; }

		public EnvironmentChangedToCompleted(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
