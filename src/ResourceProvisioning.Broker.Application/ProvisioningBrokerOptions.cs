using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Application
{
	public class ProvisioningBrokerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }
	}
}
