using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Host.Api.Configuration
{
	public class ProvisioningBrokerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }
	}
}
