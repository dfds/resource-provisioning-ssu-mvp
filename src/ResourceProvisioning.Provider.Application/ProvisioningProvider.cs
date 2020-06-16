using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Provider.Application
{
	public class ProvisioningProvider : IProvisioningProvider
	{
		public Guid Id => Guid.NewGuid();

		public GridActorType ActorType => GridActorType.System;

		public Task<IProvisioningResponse> Handle(IProvisioningRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
