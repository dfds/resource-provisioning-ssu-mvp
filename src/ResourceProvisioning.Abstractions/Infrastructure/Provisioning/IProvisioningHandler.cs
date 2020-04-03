using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	interface IProvisioningHandler : IGridActor, IEventHandler<IProvisioningEvent>
	{
	}
}
