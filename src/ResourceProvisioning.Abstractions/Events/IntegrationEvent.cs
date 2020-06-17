using System;
using System.Text.Json;

namespace ResourceProvisioning.Abstractions.Events
{
	public abstract class IntegrationEvent : IIntegrationEvent
	{
		public Guid Id { get; private set; } = Guid.NewGuid();

		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		public int Version { get; protected set; } = 1;

		public Guid CorrelationId { get; private set; } = Guid.NewGuid();

		public int SchemaVersion => 1;

		public string Type { get; protected set; }

		public JsonElement Payload { get; protected set; }

		protected IntegrationEvent(string type, JsonElement payload, Guid? id = default, Guid? correlationId = default, DateTime? createDate = default, int? version = default)
		{
			Type = type;
			Payload = payload;

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
