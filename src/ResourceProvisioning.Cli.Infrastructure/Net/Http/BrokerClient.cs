using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Net.Http;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Net.Http.Request;
using ResourceProvisioning.Cli.Infrastructure.Net.Http.Response;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http
{
	public class BrokerClient : RestClient, IBrokerService
	{
		public BrokerClient() : base(new SocketsHttpHandler())
		{

		}

		public async Task<ActualState> GetCurrentStateAsync(CancellationToken cancellationToken = default)
		{
			return await GetCurrentStateByEnvironmentAsync(Guid.Empty, cancellationToken);
		}

		public async Task<ActualState> GetCurrentStateByEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default)
		{
			var request = new GetEnvironmentRequest(environmentId);

			if (!(await SendAsync(request, cancellationToken) is JsonResponse response))
			{
				return await Task.FromResult(default(ActualState));
			}

			response.EnsureSuccessStatusCode();

			var payload = await response.Content;

			return JsonSerializer.Deserialize<ActualState>(payload.Document.RootElement.GetRawText());
		}

		public async Task ApplyDesiredStateAsync(
			Guid environmentId,
			DesiredState desiredState,
			CancellationToken cancellationToken = default)
		{
			var request = new ApplyDesiredStateRequest(environmentId, desiredState);
			var response = await SendAsync(request, cancellationToken);

			response.EnsureSuccessStatusCode();
		}
	}
}
