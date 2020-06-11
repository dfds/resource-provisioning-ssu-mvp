using System;
using System.Net.Http;
using System.Text.Json;
using ResourceProvisioning.Cli.Infrastructure.Net.Http.Content;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http.Request
{
	public class ApplyDesiredStateRequest : HttpRequestMessage
	{
		public ApplyDesiredStateRequest(Guid environmentId, dynamic desiredState)
		{
			Method = HttpMethod.Post;
			RequestUri = new Uri("DECIDE_ON_CONTROLLERS_WITH_EMIL", UriKind.Relative);
			Content = new JsonContent(JsonSerializer.Serialize(desiredState));
		}
	}
}
