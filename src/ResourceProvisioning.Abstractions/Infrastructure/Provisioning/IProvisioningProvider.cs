using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	public interface IProvisioningProvider : ICommandHandler<IProvisioningRequest, IProvisioningResponse>
	{

	}
}
