using ResourceProvisioning.Cli.Application.Configuration;

namespace ResourceProvisioning.Cli.Application
{
	public sealed class CliApplicationOptions
	{
		public const string Position = "SsuCli";

		public AuthenticationSection Authentication { get; set; }
	}
}
