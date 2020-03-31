using MediatR;

namespace ResourceProvisioning.Abstractions.Events.Domain
{
	public interface IDomainEvent : IEvent, INotification
	{

	}
}
