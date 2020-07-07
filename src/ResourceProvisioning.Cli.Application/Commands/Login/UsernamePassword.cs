using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command(Name="up")]
	public sealed class UsernamePassword : AuthenticationCommand<UsernamePasswordFlow>
	{
		public UsernamePassword(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}
	}
}
