using System;

namespace ResourceProvisioning.Abstractions.Events
{
	public abstract class IntegrationEvent : IIntegrationEvent
	{
		public Guid Id { get; private set; } = Guid.NewGuid();

		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		public int Version { get; protected set; } = 1;

		public Guid CorrelationId => throw new NotImplementedException();

		public int SchemaVersion => 1;

		public string Type => throw new NotImplementedException();

		protected IntegrationEvent(Guid? id = null, DateTime? createDate = null, int? version = null)
		{
			if (id.HasValue)
			{
				Id = id.Value;
			}

			if (createDate.HasValue)
			{
				CreationDate = createDate.Value;
			}

			if (createDate.HasValue)
			{ 
				CreationDate = createDate.Value;
			}

			if(version.HasValue)
			{ 
				Version = version.Value;
			}
		}
	}
}
