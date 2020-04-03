using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BeHeroes.MicroServices.EventBus.Abstractions;
using BeHeroes.OrderProcessing.Host.Worker.Application.IntegrationEvents;
using BeHeroes.OrderProcessing.Host.Worker.Configuration;
using BeHeroes.OrderProcessing.Host.Worker.Resources;

namespace BeHeroes.OrderProcessing.Host.Worker.Services
{
	public class GracePeriodManagerService : BackgroundService
	{
		private const string GracePeriodManagerStartingResourceKey = "SERVICE_STARTING";
		private const string GracePeriodManagerStoppingResourceKey = "SERVICE_STOPPING";
		private const string GracePeriodManagerWorkingResourceKey = "SERVICE_WORKING";
		private const string GracePeriodManagerCheckingGracePeriodResourceKey = "CHECKING_GRACE_PERIOD";
		private const string GracePeriodManagerPublishGracePeriodIntegrationEventResourceKey = "PUBLISH_EVENT";

		private readonly ILogger<GracePeriodManagerService> _logger;
		private readonly GracePeriodeManagerOptions _options;
		private readonly IEventBus _eventBus;

		public GracePeriodManagerService(IOptions<GracePeriodeManagerOptions> options,
										 IEventBus eventBus,
										 ILogger<GracePeriodManagerService> logger)
		{
			_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
			_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerStartingResourceKey));

			stoppingToken.Register(() => _logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerStoppingResourceKey)));

			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerWorkingResourceKey));

				CheckConfirmedGracePeriodOrders();

				await Task.Delay(_options.CheckUpdateTime, stoppingToken);
			}

			_logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerStoppingResourceKey));

			await Task.CompletedTask;
		}

		private void CheckConfirmedGracePeriodOrders()
		{
			_logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerCheckingGracePeriodResourceKey));

			var orderIds = GetConfirmedGracePeriodOrders();

			foreach (var orderId in orderIds)
			{
				var confirmGracePeriodEvent = new GracePeriodConfirmedIntegrationEvent(orderId);

				_logger.LogDebug(Services_GracePeriodManager.ResourceManager?.GetString(GracePeriodManagerPublishGracePeriodIntegrationEventResourceKey), confirmGracePeriodEvent);

				_eventBus.Publish(confirmGracePeriodEvent);
			}
		}

		private IEnumerable<Guid> GetConfirmedGracePeriodOrders()
		{
			//TODO: Implement MaterializedView pattern (db read logic)
			return Enumerable.Empty<Guid>();
		}
	}
}
