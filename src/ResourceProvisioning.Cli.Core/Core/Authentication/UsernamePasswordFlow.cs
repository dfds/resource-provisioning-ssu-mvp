using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Core.Core.Authentication
{
	// Doesn't work with MFA. Intended for legacy ServiceAccount usage.
	public class UsernamePasswordFlow : IAuthentication
	{
		private HttpClient _httpClient;

		public UsernamePasswordFlow()
		{
			_httpClient = new HttpClient();
		}

		public async Task<AuthenticationToken> Auth()
		{
			var userData = GetUserData();
			var response = await Login(userData);

			return new AuthenticationToken
			{
				AccessToken = response.AccessToken,
				IdToken = response.IdToken,
				Scope = response.Scope,
				ExpiresIn = response.ExpiresIn,
				TokenType = response.TokenType
			};
		}

		private UserData GetUserData()
		{
			var envUser = System.Environment.GetEnvironmentVariable("SSU_USER");
			var envPass = System.Environment.GetEnvironmentVariable("SSU_PASSWORD");

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

		private async Task<InteractiveFlowTokenValidResponse> Login(UserData input)
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("scope", "user.read openid profile");
			dict.Add("username", input.Username);
			dict.Add("password", input.Password);
			dict.Add("grant_type", "password");
			// TODO: Don't hardcode URI
			var resp = await _httpClient.PostAsync("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/token", new FormUrlEncodedContent(dict));
			var respPayload = await resp.Content.ReadAsStringAsync();
			
			if (resp.IsSuccessStatusCode)
			{
				var response = JsonSerializer.Deserialize<InteractiveFlowTokenValidResponse>(respPayload);
				return response;
			}
			
			Console.WriteLine(respPayload);
			throw new Exception(respPayload);

		}
	}

	class InteractiveFlowTokenValidResponse // 200
	{
		[JsonPropertyName("token_type")]
		public string TokenType { get; set; }
		[JsonPropertyName("scope")]
		public string Scope { get; set; }
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }
		[JsonPropertyName("id_token")]
		public string IdToken { get; set; }
		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }
		[JsonPropertyName("refresh_token")]
		public int RefreshToken { get; set; }
	}
}
