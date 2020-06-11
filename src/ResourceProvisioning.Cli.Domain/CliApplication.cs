using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Infrastructure.Net.Http;

namespace ResourceProvisioning.Cli.Application
{
	public class CliApplication : CommandLineApplication<CliApplication>
    {
        public CliApplication(IServiceCollection services = default)
        {
            if (services == null)
            {
                services = new ServiceCollection()
                                .AddSingleton<IBrokerClient>(new BrokerClient());
            }

            var serviceProvider = services.BuildServiceProvider();

            //Setup CommandLineUtils with inversion of control.
            Conventions.UseDefaultConventions().UseConstructorInjection(serviceProvider);
        }
    }
}
