using System;
using System.Net.Http;

namespace ResourceProvisioning.Cli.Infrastructure.Protocols.Http.Request
{
	public class GetEnvironmentRequest : HttpRequestMessage
	{
		public GetEnvironmentRequest(Guid environmentId = default)
		{
			Method = HttpMethod.Get;
			RequestUri = new Uri($"http://localhost:50900/controlplane?environmentId={environmentId}", UriKind.Absolute);
		}
	}
}
