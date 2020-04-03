using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BeHeroes.MicroServices.EventBus.Abstractions;
using BeHeroes.OrderProcessing.Host.Worker.Application.Tasks;
using BeHeroes.OrderProcessing.Host.Worker.Configuration;

namespace BeHeroes.OrderProcessing.Host.Worker.Services
{
	public static class TaskSchedulerServiceExtensions
	{
		public static IServiceCollection AddScheduler(this IServiceCollection services, Action<SchedulerOptions> configureOptions)
		{
			return AddScheduler(services, configureOptions, null);
		}

		public static IServiceCollection AddScheduler(this IServiceCollection services, Action<SchedulerOptions> configureOptions, EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskExceptionHandler)
		{
			services.Configure(configureOptions);

			return services.AddSingleton<IHostedService, TaskSchedulerService>(serviceProvider =>
			{
				var instance = new TaskSchedulerService(serviceProvider.GetServices<IScheduledTask>(),
															serviceProvider.GetService<IOptions<SchedulerOptions>>(),
															serviceProvider.GetService<IEventBus>(),
															serviceProvider.GetService<ILogger<TaskSchedulerService>>());

				if(unobservedTaskExceptionHandler!= null)
				{ 
					instance.UnobservedTaskException += unobservedTaskExceptionHandler;
				}

				return instance;
			});
		}
	}
}
