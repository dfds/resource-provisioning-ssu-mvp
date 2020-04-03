namespace ResourceProvisioning.Abstractions.Events
{
	public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler, IEventHandler<TEvent> where TEvent : IIntegrationEvent
	{

	}

	public interface IIntegrationEventHandler
	{
	}
}
