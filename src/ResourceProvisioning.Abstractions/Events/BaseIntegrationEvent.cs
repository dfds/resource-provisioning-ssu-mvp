using System;
using Newtonsoft.Json;

namespace ResourceProvisioning.Abstractions.Events
{
	public abstract class BaseIntegrationEvent : IIntegrationEvent
	{
		[JsonProperty]
		public Guid Id { get; private set; } = Guid.NewGuid();

		[JsonProperty]
		public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

		[JsonProperty]
		public int Version { get; protected set; } = 1;

		[JsonConstructor]
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
