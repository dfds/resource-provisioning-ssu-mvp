using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ProvisioningBrokerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }
	}
}
