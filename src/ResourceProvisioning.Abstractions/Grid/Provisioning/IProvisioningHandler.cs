using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningHandler : ICommandHandler<IProvisioningRequest, IProvisioningResponse>, IGridActor
	{

	}
}
