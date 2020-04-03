using MediatR;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IDomainEvent : IEvent, INotification
	{

	}
}
