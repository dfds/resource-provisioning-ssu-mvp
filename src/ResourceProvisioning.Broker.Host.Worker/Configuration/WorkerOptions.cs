using Microsoft.Extensions.Configuration;

namespace BeHeroes.OrderProcessing.Host.Worker.Configuration
{
	public class WorkerOptions
	{
		public IConfigurationSection ConnectionStrings { get; set; }
	}
}
