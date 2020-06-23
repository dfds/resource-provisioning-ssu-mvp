using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Commands;
using ResourceProvisioning.Cli.Application.Commands.Get;
using ResourceProvisioning.Cli.Application.Commands.Login;

namespace ResourceProvisioning.Cli.Application
{
	[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
	[Subcommand(typeof(Login))]
	[Subcommand(typeof(Apply))]
	[Subcommand(typeof(Get))]
	public class CliApplication : CliCommand<Task<int>>
	{
		public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(0);
		}

		private static string GetVersion() => typeof(CliApplication).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
	}
}
