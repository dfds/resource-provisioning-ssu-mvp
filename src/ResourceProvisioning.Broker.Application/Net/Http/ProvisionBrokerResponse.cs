using System.Net.Http;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Net.Http
{
	public sealed class ProvisionBrokerResponse : HttpResponseMessage, IProvisioningResponse
	{
		public ProvisionBrokerResponse()
		{
			StatusCode = System.Net.HttpStatusCode.OK;
		}
	}
}
