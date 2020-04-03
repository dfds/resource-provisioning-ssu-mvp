using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Host.Api.Configuration
{
	public class BrokerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }
	}
}
