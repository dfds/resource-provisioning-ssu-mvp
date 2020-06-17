using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Telemetry;

namespace ResourceProvisioning.Broker.Infrastructure.Telemetry
{
	public sealed class AppInsightsTelemetryProvider : ITelemetryProvider
	{
		private readonly TelemetryConfiguration _telemetryConfiguration;

		public AppInsightsTelemetryProvider(IOptions<TelemetryConfiguration> telemetryConfiguration)
		{
			_telemetryConfiguration = telemetryConfiguration?.Value;
		}

		public T GetClient<T>() where T : class
		{
			return new TelemetryClient(_telemetryConfiguration) as T;
		}
	}
}
