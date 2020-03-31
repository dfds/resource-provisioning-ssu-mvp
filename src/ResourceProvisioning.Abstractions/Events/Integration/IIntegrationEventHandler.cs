namespace ResourceProvisioning.Abstractions.Events.Integration
{
	public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler, IEventHandler<TEvent> where TEvent : IIntegrationEvent
	{

	}

	public interface IIntegrationEventHandler
	{
	}
}
