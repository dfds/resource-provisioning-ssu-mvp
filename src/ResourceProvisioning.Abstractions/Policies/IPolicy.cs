using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Policies
{
	public interface IPolicy : IDomainEventHandler<IDomainEvent>
	{
	}
}
