using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ResourceProvisioning.Broker.Host.Worker
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		//TODO: Inject required dependencies.
		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

				//TODO: Fetch events from kafka.
				//TODO: Update aggregates based on fetched events.
				//TODO: Save updated aggregates.
				//TODO: Dispatch environment updated event.

				//TODO: Make delay configurable.
				await Task.Delay(1000, stoppingToken);
			}
		}
	}
}
