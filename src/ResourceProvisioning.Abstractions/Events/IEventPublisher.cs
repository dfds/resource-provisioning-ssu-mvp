using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IEventPublisher
	{
		Task Publish(IEvent @event);
	}
}
