using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeHeroes.OrderProcessing.Worker.Host.Services;

namespace BeHeroes.OrderProcessing.Host.Worker.Services
{
	public static class ApplicationLifetimeEventHandlerServiceExtensions
	{
		public static IServiceCollection AddApplicationLifetimeEventHandler(this IServiceCollection services)
		{
			services.AddSingleton<IHostedService, ApplicationLifetimeEventHandlerService>();

			return services;
		}
	}
}
