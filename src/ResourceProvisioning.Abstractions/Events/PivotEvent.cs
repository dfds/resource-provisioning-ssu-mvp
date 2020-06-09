using System;

namespace ResourceProvisioning.Abstractions.Events
{
	public abstract class PivotEvent : IPivotEvent
	{
		public Guid Id { get; private set; } = Guid.NewGuid();

		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		public int Version { get; protected set; } = 1;

		public Guid CorrelationId => throw new NotImplementedException();

		public int SchemaVersion => throw new NotImplementedException();

		public string EventType => throw new NotImplementedException();

		protected PivotEvent(Guid? id = null, DateTime? createDate = null, int? version = null)
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
