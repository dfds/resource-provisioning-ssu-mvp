using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningProvider : ICommandHandler<IProvisioningRequest, IProvisioningResponse>
	{

	}
}
