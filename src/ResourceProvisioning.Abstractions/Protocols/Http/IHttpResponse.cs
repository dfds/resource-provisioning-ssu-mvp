using System.Net;

namespace ResourceProvisioning.Abstractions.Protocols.Http
{
	public interface IHttpResponse
	{
		HttpStatusCode StatusCode { get; }
	}
}
