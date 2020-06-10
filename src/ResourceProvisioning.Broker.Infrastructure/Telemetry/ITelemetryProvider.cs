namespace ResourceProvisioning.Broker.Infrastructure.Telemetry
{
	public interface ITelemetryProvider
	{
		T GetClient<T>() where T : class;
	}
}
