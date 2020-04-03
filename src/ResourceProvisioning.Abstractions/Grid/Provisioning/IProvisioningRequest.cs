using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningRequest : ICommand<IProvisioningResponse>
	{
	}
}
