using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
				var response = JsonConvert.DeserializeObject<InteractiveFlowTokenValidResponse>(respPayload);
				return response;
			}
			
			Console.WriteLine(respPayload);
			throw new Exception(respPayload);

		}
	}

	class UserData
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
	
	class InteractiveFlowTokenValidResponse // 200
	{
		[JsonProperty("token_type")]
		public string TokenType { get; set; }
		[JsonProperty("scope")]
		public string Scope { get; set; }
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		[JsonProperty("id_token")]
		public string IdToken { get; set; }
		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
		[JsonProperty("refresh_token")]
		public int RefreshToken { get; set; }
	}
}
