using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Protocols.Http;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Net.Protocols.Request;
using ResourceProvisioning.Cli.Infrastructure.Protocols.Http.Request;

namespace ResourceProvisioning.Cli.Infrastructure.Protocols.Http
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

			try
			{
				var r = await new HttpClient().GetAsync(request.RequestUri);

				var x = await r.Content.ReadAsStringAsync();
				Console.WriteLine(x);
				//var response = await SendAsync(request, cancellationToken);

				//if (!response.IsSuccessStatusCode)
				//{
				//	return await Task.FromResult(default(ActualState));
				//}

				//var payload = await response.Content.ReadAsStringAsync();
				//Console.WriteLine("Payload");
				//Console.WriteLine(payload);
				//Console.WriteLine(JsonSerializer.Deserialize<ActualState>(payload));
				//return JsonSerializer.Deserialize<ActualState>(payload);
			}
			catch (Exception e) 
			{
				throw e;
			}

			return null;
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
