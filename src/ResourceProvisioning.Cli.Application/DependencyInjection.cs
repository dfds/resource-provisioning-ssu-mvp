using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Protocols.Http;

namespace ResourceProvisioning.Cli.Application
{
	public static class DependencyInjection
	{
		public static void AddCli(this IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json", true, true)
				.AddUserSecrets(Assembly.GetExecutingAssembly(), true)
				.Build();
			
			services.ConfigureCli(configuration);
			services.AddServices();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IBrokerService, BrokerClient>();
		}

		private static void ConfigureCli(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<CliApplicationOptions>(configuration.GetSection(CliApplicationOptions.Position));
		}
	}
}
