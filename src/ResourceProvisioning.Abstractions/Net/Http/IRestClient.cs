using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public interface IRestClient : IDisposable
	{
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
	}
}
