using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	public sealed class Basic : AuthenticationCommand<UsernamePasswordFlow>
	{		
		public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			return await base.OnExecuteAsync(cancellationToken);
		}
	}
}
