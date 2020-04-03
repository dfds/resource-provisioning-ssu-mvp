using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	interface IProvisioningProvider : ICommandHandler<IProvisioningRequest, IProvisioningResponse>
	{

	}
}
