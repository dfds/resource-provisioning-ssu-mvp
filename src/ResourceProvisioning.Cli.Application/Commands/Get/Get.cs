using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ResourceProvisioning.Cli.Application.Commands.Get
{
	[Command()]
	[Subcommand(typeof(GetEnvironment))]
	public sealed class Get : CliCommand<Task<int>>
	{
		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(0);
		}
	}
}
