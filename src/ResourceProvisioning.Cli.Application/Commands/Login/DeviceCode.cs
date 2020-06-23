using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	public sealed class DeviceCode : AuthenticationCommand<DeviceCodeFlow>
	{
	}
}
