using System.Net.Http;
using System.Text.Json;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Protocols.Http
{
	public sealed class ProvisioningBrokerResponse : HttpResponseMessage, IProvisioningResponse
	{
		public ProvisioningBrokerResponse(dynamic content = null, JsonSerializerOptions options = default)
		{
			StatusCode = System.Net.HttpStatusCode.OK;
			
			if(content != null)
			{ 
				Content = new StringContent(JsonSerializer.Serialize(content, options));
			}
		}
	}
}
