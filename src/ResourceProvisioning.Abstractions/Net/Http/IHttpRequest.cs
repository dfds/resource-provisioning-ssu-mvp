using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public interface IHttpRequest
	{
		Uri RequestUri { get; }

		HttpMethod Method { get; }

		HttpContent Content { get; }

		HttpRequestHeaders Headers { get; }
	}
}
