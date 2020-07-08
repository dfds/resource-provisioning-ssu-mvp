using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swan.Logging;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	//TODO: Ensure EmbedIO server doesn't screw the user's terminal
	public partial class InteractiveFlow : AuthenticationProvider
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

			//TODO: Fix these values later.
			return new JwtSecurityToken(issuer: "ssucli", audience: "serviceBroker");
		}

		private Task ShowBrowser()
		{
			var browser = new Process
			{
				//TODO: Move to config.
				StartInfo = new ProcessStartInfo("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/authorize?client_id=72d0443b-ff34-4568-8eb9-1d81849c5462&response_type=token&redirect_uri=http%3A%2F%2Flocalhost%3A47561%2Fredirect&scope=api%3A%2F%2F72d0443b-ff34-4568-8eb9-1d81849c5462%2Faccess_as_user&nonce=12345")
				{
					UseShellExecute = true
				}
			};

			browser.Start();

			return Task.CompletedTask;
		}
	}
}
