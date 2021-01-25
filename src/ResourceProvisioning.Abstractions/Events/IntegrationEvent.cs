using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ResourceProvisioning.Abstractions.Events
{
	public class IntegrationEvent : IIntegrationEvent
	{
		public Guid Id { get; private set; } = Guid.NewGuid();

		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		public int Version { get; protected set; } = 1;

		public Guid CorrelationId { get; private set; } = Guid.NewGuid();

		public int SchemaVersion => 1;

		public string Type { get; protected set; }

		public JsonElement? Payload { get; protected set; }

		public IEnumerable<string> Topics { get; protected set; }

		public IntegrationEvent(string type, JsonElement payload, Guid? id = default, Guid? correlationId = default, DateTime? createDate = default, int? version = default, IEnumerable<string> topics = default)
		{
			Type = type;
			Payload = payload;
			Topics = topics;

			if (id.HasValue)
			{
				Id = id.Value;
			}

			if (correlationId.HasValue)
			{
				CorrelationId = correlationId.Value;
			}

			if (createDate.HasValue)
			{
				CreationDate = createDate.Value;
			}

			if (createDate.HasValue)
			{
				CreationDate = createDate.Value;
			}

			if (version.HasValue)
			{
				Version = version.Value;
			}
		}
	}
}
