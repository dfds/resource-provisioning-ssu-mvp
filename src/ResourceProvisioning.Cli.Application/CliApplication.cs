using System;
using Datadog.Trace;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace ResourceProvisioning.Cli.Application
{
    public class CliApplication : CommandLineApplication<Program>
    {
        public CliApplication(IServiceCollection services = default)
        {
            if (services == null)
            {
                services = new ServiceCollection()
                                .AddSingleton(RestClient.Core.Factories.RestClientFactory.CreateFromBaseUri(new Uri("https://ssu-broker.hellman.oxygen.dfds.cloud/state")));
            }

            var serviceProvider = services.BuildServiceProvider();

            //Setup CommandLineUtils with inversion of control.
            Conventions.UseDefaultConventions().UseConstructorInjection(serviceProvider);

            //Setup DataDog tracer.
            Tracer.Instance = serviceProvider.GetService<Tracer>() ?? Tracer.Instance;
        }
    }
}
