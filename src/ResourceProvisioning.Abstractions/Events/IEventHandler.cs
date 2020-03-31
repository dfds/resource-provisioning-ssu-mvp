using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
	{
		Task Handle(TEvent @event);
	}

	public interface IEventHandler
	{
	}
}
