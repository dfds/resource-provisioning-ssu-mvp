using System;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IIntegrationEvent : IEvent
	{
		Guid Id { get; }

		Guid CorrelationId { get; }

		DateTime CreationDate { get; }

		int SchemaVersion { get; }

		string Type { get; }
	}
}
