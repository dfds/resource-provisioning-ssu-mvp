using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningHandler : IGridActor, IEventHandler<IProvisioningEvent>
	{
	}
}
