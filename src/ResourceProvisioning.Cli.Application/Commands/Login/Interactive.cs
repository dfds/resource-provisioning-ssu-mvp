using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Cli.Application.Authentication.Flows;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	public sealed class Interactive : AuthenticationCommand<InteractiveFlow>
	{
		public Interactive(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}
	}
}
