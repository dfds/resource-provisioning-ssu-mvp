using System.Net.Http;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public sealed class ProvisionBrokerResponse : HttpResponseMessage, IProvisioningResponse
	{
		public ProvisionBrokerResponse() : base()
		{
			StatusCode = System.Net.HttpStatusCode.OK;
		}
	}
}
