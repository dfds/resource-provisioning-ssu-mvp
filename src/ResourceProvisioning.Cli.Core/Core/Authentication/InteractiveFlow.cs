using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Swan.Logging;

namespace ResourceProvisioning.Cli.Core.Core.Authentication
{
	//TODO: Ensure EmbedIO server doesn't screw the user's terminal
	public class InteractiveFlow : IAuthentication
	{
		public async Task<AuthenticationToken> Auth()
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
				await server.RunAsync().ConfigureAwait(false);
			}

			throw new NotImplementedException();
		}

		private async Task Login(UserData input)
		{
			throw new NotImplementedException();
		}

		private async Task ShowBrowser()
		{
			var browser = new Process();
			browser.StartInfo = new ProcessStartInfo("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/authorize?client_id=72d0443b-ff34-4568-8eb9-1d81849c5462&response_type=token&redirect_uri=http%3A%2F%2Flocalhost%3A47561%2Fredirect&scope=api%3A%2F%2F24420be9-46e5-4584-acd7-64850d2f2a03%2Faccess_as_user&nonce=12345")
			{
				UseShellExecute = true
			};
			browser.Start();
		}

		private async Task StartServer()
		{
			
		}
	}

	class UserData
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
