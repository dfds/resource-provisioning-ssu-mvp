using System;
using Newtonsoft.Json;
using BeHeroes.MicroServices.Abstractions.Events.Integration;

namespace BeHeroes.OrderProcessing.Host.Worker.Application.IntegrationEvents
{
	public class GracePeriodConfirmedIntegrationEvent : BaseIntegrationEvent
	{
		[JsonProperty]
		public Guid OrderId { get; private set; }

		public GracePeriodConfirmedIntegrationEvent(Guid orderId) => OrderId = orderId;
	}
}
