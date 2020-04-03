using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BeHeroes.MicroServices.Abstractions.Events.Integration;

namespace BeHeroes.OrderProcessing.Host.Worker.Application.IntegrationEvents
{
	public class ScheduledTaskCompletedEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid TaskId { get; private set; }

		[JsonProperty]
		public JObject Result { get; private set; }

		public ScheduledTaskCompletedEvent(Guid taskId, JObject result = null)
		{
			TaskId = taskId;
			Result = result;
		}
	}
}
