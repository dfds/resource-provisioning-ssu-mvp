using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeHeroes.OrderProcessing.Host.Worker.Configuration;

namespace BeHeroes.OrderProcessing.Host.Worker.Services
{
	public static class GracePeriodManagerServiceExtensions
	{
		public static IServiceCollection AddGracePeriodManager(this IServiceCollection services, Action<GracePeriodeManagerOptions> configureOptions)
		{
			services.Configure(configureOptions);
			services.AddSingleton<IHostedService, GracePeriodManagerService>();

			return services;
		}
	}
}
