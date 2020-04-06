using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Handler.Application
{
	public class ProvisioningHandler : IProvisioningEventHandler
	{
		public Guid Id => Guid.NewGuid();

		public GridActorType Type => GridActorType.System;

		public Task Handle(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
