using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swan.Logging;

namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	//TODO: Ensure EmbedIO server doesn't screw the user's terminal (Ch3383)
	public partial class InteractiveFlow : AuthenticationFlow
	{
		public InteractiveFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override async ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var launchBrowser = new Thread(async () =>
			{
				var threeSeconds = 3 * 1000;
				Thread.Sleep(threeSeconds);
				await ShowBrowser();
			});

			launchBrowser.Start();
			
			Logger.UnregisterLogger<ConsoleLogger>();
			Logger.NoLogging();

			using (var server = Server.CreateWebServer())
			{
				var cts = new CancellationTokenSource();
				Server.cts = cts;
				await server.RunAsync(cts.Token).ConfigureAwait(false);
			}

			return new JwtSecurityToken();
		}

		private Task ShowBrowser()
		{
			var browser = new Process
			{
				StartInfo = new ProcessStartInfo($"{CliApplicationOptions.Authentication.Instance}/{CliApplicationOptions.Authentication.TenantId}/oauth2/v2.0/authorize?client_id={CliApplicationOptions.Authentication.ClientId}&response_type=token&redirect_uri=http%3A%2F%2F{CliApplicationOptions.Authentication.HostName}%3A47561%2Fredirect&scope=api%3A%2F%2F{CliApplicationOptions.Authentication.ClientId}%2Faccess_as_user&nonce={DateTimeOffset.Now.ToUnixTimeSeconds()}")
				{
					UseShellExecute = true
				}
			};

			browser.Start();

			return Task.CompletedTask;
		}
	}
}
