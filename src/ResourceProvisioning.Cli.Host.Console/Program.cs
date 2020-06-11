using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Cli.Application.Commands;

namespace ResourceProvisioning.Cli.Application
{
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    [Subcommand(typeof(Apply))]
    public class Program : CliCommand
    {
        public static IServiceCollection RuntimeServices { get; set; }

        public async static Task Main(params string[] args) 
        {
            var app = new CliApplication(RuntimeServices);

            await app.ExecuteAsync(args);
        }

        public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(0);
        }

        private static string GetVersion() => typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }   
}
