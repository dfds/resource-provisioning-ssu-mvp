using Microsoft.ApplicationInsights;
using ResourceProvisioning.Abstractions.Telemetry;

namespace ResourceProvisioning.Cli.Infrastructure.Telemetry
{
	public sealed class AppInsightsTelemetryProvider : ITelemetryProvider
	{
		private readonly TelemetryClient _client;

		public AppInsightsTelemetryProvider(TelemetryClient client) 
		{
			_client = client;
		}

		public T GetClient<T>() where T : class
		{
			return _client as T;
		}
	}
}
