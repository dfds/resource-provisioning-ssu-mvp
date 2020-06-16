using System.Net;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public interface IHttpResponse
	{
		HttpStatusCode StatusCode { get; }
	}
}
