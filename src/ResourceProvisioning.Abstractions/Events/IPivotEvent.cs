using System;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IPivotEvent : IEvent
	{
		Guid Id { get; }

		Guid CorrelationId { get; }

		DateTime CreationDate { get; }

		int SchemaVersion { get; }

		string EventType { get; }
	}
}
