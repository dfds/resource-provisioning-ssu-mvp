using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using BeHeroes.MicroServices.EventBus.Abstractions;
using BeHeroes.OrderProcessing.Host.Worker.Application.IntegrationEvents;
using BeHeroes.OrderProcessing.Host.Worker.Application.Tasks;
using BeHeroes.OrderProcessing.Host.Worker.Configuration;
using BeHeroes.OrderProcessing.Host.Worker.Resources;

namespace BeHeroes.OrderProcessing.Host.Worker.Services
{
	public class TaskSchedulerService : BackgroundService
	{
		private const string SchedulerStartingResourceKey = "SERVICE_STARTING";
		private const string SchedulerStoppingResourceKey = "SERVICE_STOPPING";
		private const string SchedulerWorkingResourceKey = "SERVICE_WORKING";

		private readonly ILogger<TaskSchedulerService> _logger;
		private readonly SchedulerOptions _options;
		private readonly IEventBus _eventBus;
		private readonly IList<ScheduledTaskWrapper> _scheduledTasks = new List<ScheduledTaskWrapper>();

		public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

		public TaskSchedulerService(IEnumerable<IScheduledTask> scheduledTasks,
										IOptions<SchedulerOptions> options,
										IEventBus eventBus,
										ILogger<TaskSchedulerService> logger)
		{
			_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
			_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			if (scheduledTasks == null)
			{
				throw new ArgumentNullException(nameof(scheduledTasks));
			}

			var referenceTime = DateTime.UtcNow;

			foreach (var scheduledTask in scheduledTasks)
			{
				_scheduledTasks.Add(new ScheduledTaskWrapper
				{
					Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
					Task = scheduledTask,
					NextRunTime = referenceTime
				});
			}
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug(Services_TaskSchedulerService.ResourceManager?.GetString(SchedulerStartingResourceKey));

			cancellationToken.Register(() => _logger.LogDebug(Services_TaskSchedulerService.ResourceManager?.GetString(SchedulerStoppingResourceKey)));

			while (!cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug(Services_TaskSchedulerService.ResourceManager?.GetString(SchedulerWorkingResourceKey));

				await ExecuteOnceAsync(cancellationToken);

				await Task.Delay(TimeSpan.FromMilliseconds(_options.DelayBetweenTasks), cancellationToken);
			}

			_logger.LogDebug(Services_TaskSchedulerService.ResourceManager?.GetString(SchedulerStoppingResourceKey));

			await Task.CompletedTask;
		}

		private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
		{
			var taskFactory = new TaskFactory(TaskScheduler.Current);
			var referenceTime = DateTime.UtcNow;

			var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime));

			foreach (var scheduledTask in tasksThatShouldRun)
			{
				scheduledTask.Increment();

				await taskFactory.StartNew(
					async () =>
					{
						try
						{
							await scheduledTask.Task.ExecuteAsync(cancellationToken);

							await _eventBus.Publish(new ScheduledTaskCompletedEvent(scheduledTask.Task.Id));
						}
						catch (Exception ex)
						{
							var args = new UnobservedTaskExceptionEventArgs(ex as AggregateException ?? new AggregateException(ex));

							UnobservedTaskException?.Invoke(this, args);

							if (!args.Observed)
							{
								throw;
							}
						}
					},
					cancellationToken);
			}
		}

		private class ScheduledTaskWrapper
		{
			public CrontabSchedule Schedule { get; set; }

			public IScheduledTask Task { get; set; }

			public DateTime LastRunTime { get; set; }

			public DateTime NextRunTime { get; set; }

			public void Increment()
			{
				LastRunTime = NextRunTime;
				NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
			}

			public bool ShouldRun(DateTime currentTime)
			{
				return NextRunTime < currentTime && LastRunTime != NextRunTime;
			}
		}
	}
}
