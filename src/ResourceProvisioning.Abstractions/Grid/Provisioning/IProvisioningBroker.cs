using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningBroker : IProvisioningHandler, IEventHandler<IProvisioningEvent>
	{
	}
}
