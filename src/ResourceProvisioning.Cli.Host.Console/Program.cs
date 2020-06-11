using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Infrastructure.Net.Http;

namespace ResourceProvisioning.Cli.Application
{
	public class Program
	{
		public static IServiceCollection RuntimeServices { get; set; }

		public static async Task Main(params string[] args) 
        {
			var app = new CommandLineApplication<CliApplication>();

			if (RuntimeServices == null)
			{
				RuntimeServices = new ServiceCollection()
								.AddSingleton<IBrokerClient>(new BrokerClient());
			}

			app.Conventions.UseDefaultConventions().UseConstructorInjection(RuntimeServices.BuildServiceProvider());

			await Task.FromResult(app.ExecuteAsync(args));
		}
    }   
}
