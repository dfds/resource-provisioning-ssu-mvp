using System.Net.Http;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IProvisioningResponse
	{
		HttpContent Content { get; }
	}
}
