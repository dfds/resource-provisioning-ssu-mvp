using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Cli.Application.Authentication.Flows;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	[Command()]
	public sealed class DeviceCode : AuthenticationCommand<DeviceCodeFlow>
	{
		public DeviceCode(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}
	}
}
