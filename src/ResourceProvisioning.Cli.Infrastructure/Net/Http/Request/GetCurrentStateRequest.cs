using System;
using System.Net.Http;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http.Request
{
	public class GetEnvironmentRequest : HttpRequestMessage
	{
		public GetEnvironmentRequest(Guid environmentId = default)
		{
			var targetUri = (environmentId != Guid.Empty) ? "state" : "state/{environmentId}";
			
			Method = HttpMethod.Get;
			RequestUri = new Uri(targetUri, UriKind.Relative);
		}
	}
}
