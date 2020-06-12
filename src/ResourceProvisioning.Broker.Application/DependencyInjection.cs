﻿using System.Reflection;
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
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;
using ResourceProvisioning.Broker.Infrastructure.Telemetry;

namespace ResourceProvisioning.Broker.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningBroker(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			services.AddOptions();
			services.Configure(configureOptions);

			services.AddTelemetry();
			services.AddBehaviors();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddIdempotency();
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
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(IPipelineBehavior<,>));
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			services.AddTransient(typeof(ICommandHandler<,>), typeof(ICommandHandler<,>));
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{
			services.AddTransient(typeof(IEventHandler<>), typeof(IEventHandler<>));
		}

		private static void AddIdempotency(this IServiceCollection services)
		{
			services.AddTransient<IRequestManager, RequestManager>();
		}

		private static void AddPersistancy(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

			using var serviceProvider = services.BuildServiceProvider();
			var brokerOptions = serviceProvider.GetService<IOptions<ProvisioningBrokerOptions>>()?.Value;

			if (brokerOptions != null)
			{
				services.AddDbContext<DomainContext>(options =>
				{
					options.UseSqlite(brokerOptions.ConnectionStrings.GetValue<string>(nameof(DomainContext)),
										sqliteOptionsAction: sqliteOptions =>
										{
											sqliteOptions.MigrationsAssembly(callingAssemblyName);
											sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");
										});
				});
			}
			else
			{
				throw new ProvisioningBrokerException("Could not resolve provision broker options");
			}
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(IRepository<>));
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
