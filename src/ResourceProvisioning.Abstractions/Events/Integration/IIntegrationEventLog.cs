using System;

namespace ResourceProvisioning.Abstractions.Events.Integration
{
	public interface IIntegrationEventLog
	{
		Guid EventId { get; }

		string EventTypeName { get; }

		object State { get; }

		int TimesSent { get; }

		DateTime CreationDate { get; }

		string Content { get; }

		IIntegrationEventLog DeserializeJsonContent(Type type);
	}
}
