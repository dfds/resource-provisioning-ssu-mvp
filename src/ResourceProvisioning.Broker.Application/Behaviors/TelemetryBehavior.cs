﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Telemetry;

namespace ResourceProvisioning.Broker.Application.Behaviors
{
	public sealed class TelemetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<TelemetryBehavior<TRequest, TResponse>> _logger;
		private readonly ITelemetryProvider _telemetryProvider;

		public TelemetryBehavior(ITelemetryProvider telemetryProvider,
			ILogger<TelemetryBehavior<TRequest, TResponse>> logger)
		{
			_telemetryProvider = telemetryProvider ?? throw new ArgumentException(nameof(ITelemetryProvider));
			_logger = logger ?? throw new ArgumentException(nameof(ILogger));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			TResponse response;
			var client = _telemetryProvider.GetClient<TelemetryClient>();

			try
			{
				client.TrackTrace(System.Text.Json.JsonSerializer.Serialize(request, request.GetType()));

				response = await next();

				client.TrackTrace(System.Text.Json.JsonSerializer.Serialize(response, response.GetType()));
			}
			catch (Exception e)
			{
				client.TrackException(e);

				throw;
			}

			return response;
		}
	}
}
