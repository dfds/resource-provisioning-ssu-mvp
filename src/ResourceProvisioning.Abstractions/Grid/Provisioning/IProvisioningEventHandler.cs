using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningEventHandler : IEventHandler<IProvisioningEvent>
	{
	}
}
