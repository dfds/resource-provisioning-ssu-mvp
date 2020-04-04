using MediatR;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IDomainEventHandler<in TEvent> : IDomainEventHandler, INotificationHandler<TEvent>, IEventHandler<TEvent> where TEvent : IDomainEvent
	{

	}

	public interface IDomainEventHandler
	{
	}
}
