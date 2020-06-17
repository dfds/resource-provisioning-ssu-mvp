using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ProvisioningBrokerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }

		public bool EnableAutoMigrations { get; set; } = false;
	}
}
