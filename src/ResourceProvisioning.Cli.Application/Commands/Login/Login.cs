using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	[Subcommand(typeof(Basic))]
	[Subcommand(typeof(Interactive))]
	[Subcommand(typeof(DeviceCode))]
	public sealed class Login : CliCommand<Task<int>>
	{		
		public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(0);
		}
	}
}
