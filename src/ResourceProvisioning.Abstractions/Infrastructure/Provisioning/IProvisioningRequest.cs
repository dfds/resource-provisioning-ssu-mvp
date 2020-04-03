using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Infrastructure.Provisioning
{
	interface IProvisioningRequest : ICommand<IProvisioningResponse>
	{
	}
}
