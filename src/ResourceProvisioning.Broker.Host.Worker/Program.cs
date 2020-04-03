using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace BeHeroes.OrderProcessing.Host.Worker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new HostBuilderFactory()
						.Create(args)
						.UseConsoleLifetime()
						.Build();
			
			Task.WaitAll(host.RunAsync());
		}
	}
}
