using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BeHeroes.Extensions.Diagnostics.HealthChecks.Runtime;
using BeHeroes.Extensions.Diagnostics.HealthChecks.Sql;
using BeHeroes.OrderProcessing.Host.Worker.Application.Tasks;
using BeHeroes.OrderProcessing.Host.Worker.Configuration;
using BeHeroes.OrderProcessing.Host.Worker.Services;
using BeHeroes.OrderProcessing.Infrastructure.EntityFramework;

namespace BeHeroes.OrderProcessing.Host.Worker
{
	public class HostBuilderFactory
	{
		public virtual IHostBuilder Create(string[] args = null)
		{
			return new HostBuilder()
						.ConfigureHostConfiguration(configHost =>
						{
							configHost.SetBasePath(Directory.GetCurrentDirectory());
							configHost.AddJsonFile("hostsettings.json", optional: true);
							configHost.AddEnvironmentVariables();

							if (args != null)
							{
								configHost.AddCommandLine(args);
							}
						})
						.ConfigureAppConfiguration((hostContext, configApp) =>
						{
							configApp.AddJsonFile("appsettings.json", optional: true);
							configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
							configApp.AddEnvironmentVariables();

							if (args != null)
							{
								configApp.AddCommandLine(args);
							}
						})
						.ConfigureServices((hostContext, services) =>
						{
							services.AddOptions();
							services.AddDbContext<OrderProcessingContext>(options =>
							{
								options.UseSqlite(hostContext.Configuration.GetConnectionString(nameof(OrderProcessingContext)),
													sqliteOptionsAction: sqliteOptions =>
													{
														sqliteOptions.MigrationsAssembly(typeof(HostBuilderFactory).GetTypeInfo().Assembly.GetName().Name);
														sqliteOptions.MigrationsHistoryTable(typeof(HostBuilderFactory).GetTypeInfo().Assembly.GetName().Name + "MigrationHistory");
													});
							}, ServiceLifetime.Scoped);

							services.Configure<WorkerOptions>(hostContext.Configuration);

							services.AddApplicationLifetimeEventHandler();

							services.AddHealthChecks()
									.AddGCInfoCheck("GCInfoCheck", tags: new[] { "ready" })
									.AddCheck("Database", new SqlConnectionHealthCheck(hostContext.Configuration.GetConnectionString("OrderProcessingContext")), tags: new[] { "ready" });

							services.AddScheduler(options => hostContext.Configuration.Bind("Scheduler", options), (sender, schedulerArgs) =>
							{
								Console.Write(schedulerArgs.Exception.Message);

								schedulerArgs.SetObserved();
							});

							services.AddGracePeriodManager(options => hostContext.Configuration.Bind("GracePeriodeManager", options));

							services.AddSingleton<IScheduledTask, SomeTask>();
						})
						.ConfigureLogging((hostContext, configLogging) =>
						{
							configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
							configLogging.AddConsole();
						});
		}
	}
}
