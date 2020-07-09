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

		private ValueTask<TokenValidResponse> Login(UserData input)
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("scope", "user.read openid profile");
			dict.Add("username", input.Username);
			dict.Add("password", input.Password);
			dict.Add("grant_type", "password");

			// TODO: Don't hardcode URI
			var getTokenTask = HttpClient.PostAsync("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/token", new FormUrlEncodedContent(dict));

			getTokenTask.Wait();

			var resp = getTokenTask.Result;
			var parsePayloadTask = resp.Content.ReadAsStringAsync();
			
			if (resp.IsSuccessStatusCode)
			{
				parsePayloadTask.Wait();

				Console.WriteLine(parsePayloadTask.Result);

				var response = JsonSerializer.Deserialize<TokenValidResponse>(parsePayloadTask.Result);

				return new ValueTask<TokenValidResponse>(response);
			}
			
			throw new HttpRequestException();
		}
	}
}
