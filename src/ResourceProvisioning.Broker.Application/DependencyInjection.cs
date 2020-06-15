using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Abstractions.Telemetry;
using ResourceProvisioning.Broker.Application.Behaviors;
using ResourceProvisioning.Broker.Application.Commands.Environment;
using ResourceProvisioning.Broker.Application.Events.Environment;
using ResourceProvisioning.Broker.Application.Events.Resource;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;
using ResourceProvisioning.Broker.Domain.Events.Environment;
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Infrastructure.Repositories;
using ResourceProvisioning.Broker.Infrastructure.Telemetry;

namespace ResourceProvisioning.Broker.Application
{
	public sealed static class DependencyInjection
	{
		public static void AddProvisioningBroker(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			//Setup external DI configurations.
			services.AddLogging();
			services.AddOptions();
			services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyInjection)));
			services.AddMediatR(typeof(DependencyInjection));

			//Setup application DI configuration.
			services.AddTelemetry();
			services.AddBehaviors();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddPersistancy(configureOptions);
			services.AddRepositories();
			services.AddServices();
			services.AddBroker();
		}

		private static void AddTelemetry(this IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry();
			services.AddTransient<ITelemetryProvider, AppInsightsTelemetryProvider>();
		}

		private static void AddBehaviors(this IServiceCollection services)
		{
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TelemetryBehavior<,>));
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			services.AddTransient<ICommandHandler<CreateEnvironmentCommand, EnvironmentRoot>, CreateEnvironmentCommandHandler>();
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{
			services.AddTransient<IDomainEventHandler<EnvironmentCreatedEvent>, EnvironmentCreatedEventHandler>();
			services.AddTransient<IDomainEventHandler<EnvironmentInitializingEvent>, EnvironmentInitializingEventHandler>();
			services.AddTransient<IDomainEventHandler<EnvironmentTerminatedEvent>, EnvironmentTerminatedEventHandler>();
			services.AddTransient<IDomainEventHandler<ResourceInitializingEvent>, ResourceInitializingEventHandler>();
			services.AddTransient<IDomainEventHandler<ResourceReadyEvent>, ResourceReadyEventHandler>();
			services.AddTransient<IDomainEventHandler<ResourceUnavailableEvent>, ResourceUnavailableEventHandler>();
			services.AddTransient<IIntegrationEventHandler<ResourceProvisioningCompletedEvent>, ResourceProvisioningCompletedEventHandler>();
			services.AddTransient<IIntegrationEventHandler<ResourceProvisioningRequestedEvent>, ResourceProvisioningRequestedEventHandler>();
			services.AddTransient<IIntegrationEventHandler<ResourceProvisioningTerminatedEvent>, ResourceProvisioningTerminatedEventHandler>();
		}

		private static void AddPersistancy(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions = default)
		{
			var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

			using var serviceProvider = services.BuildServiceProvider();
			var brokerOptions = serviceProvider.GetService<IOptions<ProvisioningBrokerOptions>>()?.Value;

			configureOptions?.Invoke(brokerOptions);

			services.AddDbContext<DomainContext>(options =>
			{
				if (brokerOptions != null)
				{
					options.UseSqlite(brokerOptions.ConnectionStrings.GetValue<string>(nameof(DomainContext)),
						sqliteOptions =>
						{
							sqliteOptions.MigrationsAssembly(callingAssemblyName);
							sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");
						});
				}
			});
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IRepository<EnvironmentRoot>, EnvironmentRepository>();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IControlPlaneService, ControlPlaneService>();
		}

		private static void AddBroker(this IServiceCollection services)
		{
			services.AddSingleton<IProvisioningBroker, ProvisioningBroker>();
		}
	}
}
