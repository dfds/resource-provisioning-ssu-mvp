using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Provider.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningHandler(this IServiceCollection services, System.Action<ProvisioningProviderOptions> configureOptions)
		{
			services.AddOptions();
			services.Configure(configureOptions);

			services.AddSingleton<IProvisioningProvider>();
		}
	}
}
