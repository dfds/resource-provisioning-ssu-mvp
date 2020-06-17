using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Abstractions.Telemetry;
using ResourceProvisioning.Broker.Application.Behaviors;
using ResourceProvisioning.Broker.Application.Commands.Environment;
using ResourceProvisioning.Broker.Application.Events.Environment;
using ResourceProvisioning.Broker.Application.Events.Provisioning;
using ResourceProvisioning.Broker.Application.Events.Resource;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;
using ResourceProvisioning.Broker.Domain.Events.Environment;
using ResourceProvisioning.Broker.Domain.Repository;
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Infrastructure.Repositories;
using ResourceProvisioning.Broker.Infrastructure.Telemetry;

namespace ResourceProvisioning.Broker.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningBroker(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddLogging();
			services.AddOptions();
			services.AddMediator();
			services.AddTelemetryProviders();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddPersistancy(configureOptions);
			services.AddRepositories();
			services.AddServices();
			services.AddBroker();
		}

		private static void AddMediator(this IServiceCollection services)
		{
			services.AddTransient<ServiceFactory>(p => p.GetService);
			services.AddTransient<IMediator>(p =>
			{
				return new Mediator(p.GetService<ServiceFactory>());
			});
			
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TelemetryBehavior<,>));

			services.AddTransient<IRequestHandler<GetEnvironmentCommand, IProvisioningResponse>, GetEnvironmentCommandHandler>();
			services.AddTransient<IRequestHandler<CreateEnvironmentCommand, IProvisioningResponse>, CreateEnvironmentCommandHandler>();

			services.AddTransient<INotificationHandler<EnvironmentRequestedEvent>, EnvironmentRequestedEventHandler>();
			services.AddTransient<INotificationHandler<EnvironmentInitializingEvent>, EnvironmentInitializingEventHandler>();
			services.AddTransient<INotificationHandler<EnvironmentCreatedEvent>, EnvironmentCreatedEventHandler>();
			services.AddTransient<INotificationHandler<EnvironmentTerminatedEvent>, EnvironmentTerminatedEventHandler>();
			services.AddTransient<INotificationHandler<ResourceInitializingEvent>, ResourceInitializingEventHandler>();
			services.AddTransient<INotificationHandler<ResourceReadyEvent>, ResourceReadyEventHandler>();
			services.AddTransient<INotificationHandler<ResourceUnavailableEvent>, ResourceUnavailableEventHandler>();
			services.AddTransient<INotificationHandler<ProvisioningRequestedEvent>>(factory => factory.GetRequiredService<IProvisioningBroker>());
		}

		private static void AddTelemetryProviders(this IServiceCollection services)
		{		
			services.AddTransient<ITelemetryProvider, AppInsightsTelemetryProvider>();
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			services.AddTransient<ICommandHandler<GetEnvironmentCommand, IProvisioningResponse>, GetEnvironmentCommandHandler>();
			services.AddTransient<ICommandHandler<CreateEnvironmentCommand, IProvisioningResponse>, CreateEnvironmentCommandHandler>();
			services.AddTransient<ICommandHandler<IProvisioningRequest, IProvisioningResponse>>(factory => factory.GetRequiredService<IProvisioningBroker>());
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{
			services.AddTransient<IEventHandler<EnvironmentRequestedEvent>, EnvironmentRequestedEventHandler>();
			services.AddTransient<IEventHandler<EnvironmentInitializingEvent>, EnvironmentInitializingEventHandler>();
			services.AddTransient<IEventHandler<EnvironmentCreatedEvent>, EnvironmentCreatedEventHandler>();
			services.AddTransient<IEventHandler<EnvironmentTerminatedEvent>, EnvironmentTerminatedEventHandler>();
			services.AddTransient<IEventHandler<ResourceInitializingEvent>, ResourceInitializingEventHandler>();
			services.AddTransient<IEventHandler<ResourceReadyEvent>, ResourceReadyEventHandler>();
			services.AddTransient<IEventHandler<ResourceUnavailableEvent>, ResourceUnavailableEventHandler>();
			services.AddTransient<IEventHandler<IProvisioningEvent>>(factory => factory.GetRequiredService<IProvisioningBroker>());
		}

		private static void AddPersistancy(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions = default)
		{
			using var serviceProvider = services.BuildServiceProvider();
			var brokerOptions = serviceProvider.GetService<IOptions<ProvisioningBrokerOptions>>()?.Value;

			configureOptions?.Invoke(brokerOptions);

			services.AddDbContext<DomainContext>(options =>
			{
				var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
				var connectionString = brokerOptions?.ConnectionStrings?.GetValue<string>(nameof(DomainContext)) ?? "Filename=:memory:;";

				services.AddSingleton(factory =>
				{
					var connection = new SqliteConnection(connectionString);

					connection.Open();

					return connection;
				});

				var dbOptions = options.UseSqlite(services.BuildServiceProvider().GetService<SqliteConnection>(),
					sqliteOptions =>
					{
						sqliteOptions.MigrationsAssembly(callingAssemblyName);
						sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");
						
					}).Options;

				using var context = new DomainContext(dbOptions, new FakeMediator());

				context.Database.EnsureCreated();
			});

			services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<DomainContext>());
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IRepository<EnvironmentRoot>, EnvironmentRepository>();
			services.AddTransient<IEnvironmentRepository, EnvironmentRepository>();
			services.AddTransient<IRepository<ResourceRoot>, ResourceRepository>();
			services.AddTransient<IResourceRepository, ResourceRepository>();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IControlPlaneService, ControlPlaneService>();
		}

		private static void AddBroker(this IServiceCollection services)
		{
			services.AddTransient<IProvisioningBroker, ProvisioningBroker>();
		}
	}
}
