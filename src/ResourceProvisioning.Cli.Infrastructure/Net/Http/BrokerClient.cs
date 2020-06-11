using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Net.Http;
using ResourceProvisioning.Cli.Infrastructure.Net.Http.Request;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http
{
	public class BrokerClient : RestClient, IBrokerClient
    {
	    public BrokerClient() : base(new SocketsHttpHandler())
	    {

	    }

		public async Task<dynamic> GetCurrentStateAsync(CancellationToken cancellationToken = default)
		{
			return await GetCurrentStateByEnvironmentAsync(Guid.Empty, cancellationToken);
		}

		public async Task<dynamic> GetCurrentStateByEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default)
		{
			var request = new GetEnvironmentRequest(environmentId);
			var response = (JsonResponse)await SendAsync(request, cancellationToken);

			response.EnsureSuccessStatusCode();

			var payload = await response.Content.ConfigureAwait(false);

			return payload.Document;
		}

		public async Task ApplyDesiredStateAsync(
			Guid environmentId,
			dynamic desiredState,
			CancellationToken cancellationToken = default)
		{
			var request = new ApplyDesiredStateRequest(environmentId, desiredState);
			var response = await SendAsync(request, cancellationToken);

			response.EnsureSuccessStatusCode();
		}
    }
}
