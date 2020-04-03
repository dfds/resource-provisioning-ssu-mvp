using System.Collections.Generic;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	interface IProvisioningBroker : IProvisioningProvider, IGridActor
	{
		IEnumerable<IProvisioningHandler> Handlers { get; }
	}
}
