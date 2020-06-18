using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Protocols.Http;

namespace ResourceProvisioning.Cli.Application
{
	public static class DependencyInjection
	{
		public static void AddCli(this IServiceCollection services)
		{
			services.AddServices();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IBrokerService, BrokerClient>();
		}
	}
}
