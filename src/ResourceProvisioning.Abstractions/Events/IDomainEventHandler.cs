namespace ResourceProvisioning.Abstractions.Events
{
	public interface IDomainEventHandler<in TEvent> : IDomainEventHandler, IEventHandler<TEvent> where TEvent : IDomainEvent
	{

	}

	public interface IDomainEventHandler
	{
	}
}
