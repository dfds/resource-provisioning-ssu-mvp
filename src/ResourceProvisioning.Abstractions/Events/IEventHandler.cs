using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
	{
		Task Handle(TEvent @event, CancellationToken cancellationToken = default);
	}

	public interface IEventHandler
	{
	}
}
