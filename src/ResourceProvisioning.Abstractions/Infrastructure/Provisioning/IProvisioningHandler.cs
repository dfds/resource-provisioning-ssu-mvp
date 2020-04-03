using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	public interface IProvisioningHandler : IGridActor, IEventHandler<IProvisioningEvent>
	{
	}
}
