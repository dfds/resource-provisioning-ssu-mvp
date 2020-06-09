namespace ResourceProvisioning.Abstractions.Events
{
	public interface IPivotEventHandler<in TEvent> : IIntegrationEventHandler, IEventHandler<TEvent> where TEvent : IPivotEvent
	{

	}

	public interface IIntegrationEventHandler
	{
	}
}
