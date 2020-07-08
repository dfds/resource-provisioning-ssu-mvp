using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	// Doesn't work with MFA. Intended for legacy ServiceAccount usage.
	public partial class UsernamePasswordFlow : AuthenticationProvider
	{
		public UsernamePasswordFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override async ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var userData = GetUserData(CliApplicationOptions);
			var tokenResponse = await Login(userData);

			//TODO: Talk about this with Emil.
			return new JwtSecurityToken(tokenResponse.IdToken);
			//return new AuthenticationToken
			//{
			//	AccessToken = tokenResponse.AccessToken,
			//	IdToken = tokenResponse.IdToken,
			//	Scope = tokenResponse.Scope,
			//	ExpiresIn = tokenResponse.ExpiresIn,
			//	TokenType = tokenResponse.TokenType
			//};
		}

		private static UserData GetUserData(CliApplicationOptions cliApplicationOptions)
		{
			var envUser = cliApplicationOptions.Username;
			var envPass = cliApplicationOptions.Password;

			if (envUser != null && envPass != null)
			{
				return new UserData
				{
					Username = envUser,
					Password = envPass
				};
			}
			
			var userData = new UserData();
			Console.Write("Username: ");
			userData.Username = Console.ReadLine();
			Console.Write("Password: ");
			string password = null;
			while (true)
			{
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;
				password += key.KeyChar;
			}
			userData.Password = password;

			Console.Write("\n");
			return userData;
		}

		private async Task<TokenValidResponse> Login(UserData input)
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("scope", "user.read openid profile");
			dict.Add("username", input.Username);
			dict.Add("password", input.Password);
			dict.Add("grant_type", "password");
			//TODO: Not sure why this is breaking at the moment? Response seems to never get fired according to fiddler:
			//SESSION STATE: Aborted.
			//Response Entity Size: 0 bytes.

			//== FLAGS ==================
			//BitFlags: [ResponseGeneratedByFiddler] 0x100
			//X-ABORTED-WHEN: SendingResponse
			//X-CLIENTIP: 127.0.0.1
			//X-CLIENTPORT: 51039
			//X-EGRESSPORT: 51040
			//X-HOSTIP: 40.126.1.135
			//X-PROCESSINFO: resourceprovisioning.cli.host.console:28092
			//X-RESPONSEBODYTRANSFERLENGTH: 0

			//== TIMING INFO ============
			//ClientConnected:	19:38:31.441
			//ClientBeginRequest:	19:38:31.456
			//GotRequestHeaders:	19:38:31.457
			//ClientDoneRequest:	19:38:31.457
			//Determine Gateway:	0ms
			//DNS Lookup: 		0ms
			//TCP/IP Connect:	35ms
			//HTTPS Handshake:	0ms
			//ServerConnected:	19:38:31.492
			//FiddlerBeginRequest:	19:38:31.492
			//ServerGotRequest:	19:38:31.492
			//ServerBeginResponse:	00:00:00.000
			//GotResponseHeaders:	00:00:00.000
			//ServerDoneResponse:	00:00:00.000
			//ClientBeginResponse:	19:38:31.492
			//ClientDoneResponse:	19:38:31.492

			//	Overall Elapsed:	0:00:00.036

			//The response was buffered before delivery to the client.

			// TODO: Don't hardcode URI
			var resp = await HttpClient.PostAsync("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/token", new FormUrlEncodedContent(dict));
			var respPayload = await resp.Content.ReadAsStringAsync();
			
			if (resp.IsSuccessStatusCode)
			{
				var response = JsonSerializer.Deserialize<TokenValidResponse>(respPayload);
				return response;
			}
			
			Console.WriteLine(respPayload);
			throw new Exception(respPayload);

		}
	}
}
