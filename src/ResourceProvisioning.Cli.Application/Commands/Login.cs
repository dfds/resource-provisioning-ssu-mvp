using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands
{
	[Command(Description = "Login to the SSU broker. Defaults to a device code flow.")]

	public sealed class Login : CliCommand
	{
		private HttpClient _httpClient;

		[Option(CommandOptionType.NoValue, LongName = "devicecode", ShortName = "dc")]
		public bool DeviceCode { get; }
		[Option(CommandOptionType.NoValue, LongName = "interactive", ShortName = "int")]
		public bool Interactive { get; }
		[Option(CommandOptionType.NoValue, LongName = "authorization", ShortName = "aut")]
		public bool Authorization { get; }
		
		public Login()
		{
			_httpClient = new HttpClient();
		}

		private int AmountOfAuthOptionsSelected()
		{
			var count = 0;
			count = DeviceCode ? count +  1 : count;
			count = Interactive ? count +  1 : count;
			count = Authorization ? count +  1 : count;

			return count;
		}
		
		public async override Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			if (AmountOfAuthOptionsSelected() > 1)
			{
				Console.WriteLine("Too many methods of login has been selected. Only one of either 'devicecode', 'interactive' or 'authorization' may be selected at any time.");
				return 1;
			}

			if (AmountOfAuthOptionsSelected() == 0)
			{
				var deviceCode = new DeviceCodeFlow();
				var response = await deviceCode.Auth();
				Console.WriteLine(response.IdToken);
				return 0;
			}

			if (DeviceCode)
			{
				var deviceCode = new DeviceCodeFlow();
				var response = await deviceCode.Auth();
				Console.WriteLine(response.IdToken);
				return 0;
			}

			if (Interactive)
			{
				var interactive = new InteractiveFlow();
				var response = await interactive.Auth();
				Console.WriteLine(response.IdToken);
				return 0;
			}

			if (Authorization)
			{
				throw new NotImplementedException();
			}

			return 0;
		}
	}
}
