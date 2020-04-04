using System;

namespace ResourceProvisioning.Abstractions.Events
{
	public abstract class BaseIntegrationEvent : IIntegrationEvent
	{
		public Guid Id { get; private set; } = Guid.NewGuid();

		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		public int Version { get; protected set; } = 1;

		protected BaseIntegrationEvent(Guid? id = null, DateTime? createDate = null, int? version = null)
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
