using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	public interface IProvisioningRequest : ICommand<IProvisioningResponse>
	{
	}
}
