using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Abstractions.Telemetry;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Protocols.Http;
using ResourceProvisioning.Cli.Infrastructure.Telemetry;

namespace ResourceProvisioning.Cli.Application
{
	public static class DependencyInjection
	{
		public static void AddCli(this IServiceCollection services)
		{
			services.AddTelemetry();
			services.AddServices();
			services.AddCommandHandlers();
			services.AddRepositories();
		}

		private static void AddTelemetry(this IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry();
			services.AddTransient<ITelemetryProvider, AppInsightsTelemetryProvider>();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IBrokerService, BrokerClient>();
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			services.AddTransient(typeof(ICommandHandler<,>), typeof(ICommandHandler<,>));
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{
			services.AddTransient(typeof(IEventHandler<>), typeof(IEventHandler<>));
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(IRepository<>));
		}
	}
}
