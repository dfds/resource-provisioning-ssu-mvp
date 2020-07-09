using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	// Doesn't work with MFA. Intended for legacy ServiceAccount usage.
	public partial class UsernamePasswordFlow : AuthenticationFlow
	{
		public UsernamePasswordFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override async ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var userData = GetUserData(CliApplicationOptions);
			var tokenResponse = await Login(userData);

			return new JwtSecurityToken(tokenResponse.IdToken);
		}

		private static UserData GetUserData(CliApplicationOptions cliApplicationOptions)
		{
			var envUser = cliApplicationOptions.Authentication.Username;
			var envPass = cliApplicationOptions.Authentication.Password;

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
			dict.Add("client_id", CliApplicationOptions.Authentication.ClientId);
			dict.Add("scope", "user.read openid profile");
			dict.Add("username", input.Username);
			dict.Add("password", input.Password);
			dict.Add("grant_type", "password");

			var getTokenTask = HttpClient.PostAsync($"{CliApplicationOptions.Authentication.Instance}/{CliApplicationOptions.Authentication.TenantId}/oauth2/v2.0/token", new FormUrlEncodedContent(dict));

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
