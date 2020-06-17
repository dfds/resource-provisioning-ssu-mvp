using System;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningBroker : IProvisioningProvider, IEventHandler<IProvisioningEvent>
	{
	}
}
