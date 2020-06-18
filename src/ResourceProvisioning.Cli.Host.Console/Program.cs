using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Application;

namespace ResourceProvisioning.Cli.Host.Console
{
	public class Program
	{
		private static readonly Mutex _dependencyTracker = new Mutex();
		private static bool _waiting = false;
		private static IServiceCollection _services;

		public static IServiceCollection RuntimeServices
		{
			get 
			{
				return _services;
			}
			set
			{
				_dependencyTracker.WaitOne();
				_waiting = true;

				_services = value;
			} 
		}

		public static async Task Main(params string[] args)
		{
			var app = new CommandLineApplication<CliApplication>();

			if (RuntimeServices == null)
			{
				_services = new ServiceCollection();
				_services.AddCli();
			}

			app.Conventions.UseDefaultConventions().UseConstructorInjection(RuntimeServices.BuildServiceProvider());

			await Task.FromResult(app.ExecuteAsync(args));

			if(_waiting)
			{
				_waiting = false;
				_dependencyTracker.ReleaseMutex();				
			}
		}
	}
}
