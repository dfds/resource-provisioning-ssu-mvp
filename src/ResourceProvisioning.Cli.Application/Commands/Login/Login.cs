using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	[Subcommand(typeof(UsernamePassword))]
	[Subcommand(typeof(Interactive))]
	[Subcommand(typeof(DeviceCode))]
	public sealed class Login : CliCommand<Task<int>>
	{		
		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(0);
		}
	}
}
