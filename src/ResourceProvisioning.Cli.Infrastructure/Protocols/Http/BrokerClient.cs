using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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

		//TODO: Make to list.
		public async Task<ActualState> GetCurrentStateByEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default)
		{			
			var request = new GetEnvironmentRequest(environmentId);

			try
			{
				var response = await SendAsync(request, cancellationToken);

				if (!response.IsSuccessStatusCode)
				{
					return await Task.FromResult(default(ActualState));
				}

				var payload = JsonSerializer.Deserialize<List<ActualState>>(await response.Content.ReadAsStringAsync());

				return payload.FirstOrDefault();
			}
			catch (Exception e) 
			{
				throw e;
			}
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
