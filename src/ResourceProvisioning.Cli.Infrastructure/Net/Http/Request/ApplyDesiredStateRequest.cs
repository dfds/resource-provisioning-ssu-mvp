using System;
using System.Net.Http;
using System.Text.Json;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http.Request
{
	public class ApplyDesiredStateRequest : HttpRequestMessage
	{
		public ApplyDesiredStateRequest(Guid environmentId, dynamic desiredState)
		{
			Method = HttpMethod.Post;
			RequestUri = new Uri("state", UriKind.Relative);
			Content = new JsonContent(JsonSerializer.Serialize(desiredState));
		}
	}
}
