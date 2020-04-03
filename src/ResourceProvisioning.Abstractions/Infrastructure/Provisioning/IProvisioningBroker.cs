using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	public interface IProvisioningBroker : IProvisioningProvider, IEventHandler<IProvisioningEvent>, IGridActor
	{

	}
}
