using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Handler.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningHandler(this IServiceCollection services, System.Action<ProvisioningHandlerOptions> configureOptions)
		{
			services.AddOptions();
			services.Configure(configureOptions);

			services.AddSingleton<IProvisioningEventHandler>();
		}
	}
}
