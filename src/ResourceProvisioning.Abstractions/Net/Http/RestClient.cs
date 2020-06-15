using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public abstract class RestClient : HttpMessageInvoker, IRestClient
	{
		protected RestClient(HttpMessageHandler handler) : base(handler)
		{

		}

		protected RestClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
		{
		}

		async Task<HttpResponseMessage> IRestClient.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return await SendAsync(request, cancellationToken);
		}
	}
}
