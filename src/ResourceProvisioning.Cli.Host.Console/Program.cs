using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Application;

namespace ResourceProvisioning.Cli.Host.Console
{
	public class Program
	{
		public static IServiceCollection RuntimeServices { get; set; }

		public static async Task Main(params string[] args)
		{
			var app = new CommandLineApplication<CliApplication>();

			if (RuntimeServices == null)
			{
				RuntimeServices = new ServiceCollection();
				RuntimeServices.AddCli();
			}

			app.Conventions.UseDefaultConventions().UseConstructorInjection(RuntimeServices.BuildServiceProvider());

			await Task.FromResult(app.ExecuteAsync(args));
		}
	}
}
