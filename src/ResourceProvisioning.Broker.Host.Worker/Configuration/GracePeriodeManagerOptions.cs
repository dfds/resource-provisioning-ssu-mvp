namespace BeHeroes.OrderProcessing.Host.Worker.Configuration
{
	public class GracePeriodeManagerOptions : WorkerOptions
	{
		public int GracePeriodTime { get; set; }

		public int CheckUpdateTime { get; set; }
	}
}
