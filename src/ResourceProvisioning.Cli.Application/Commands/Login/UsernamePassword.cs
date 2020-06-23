using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command(Name="up")]
	public sealed class UsernamePassword : AuthenticationCommand<UsernamePasswordFlow>
	{		
	}
}
