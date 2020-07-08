using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public partial class DeviceCodeFlow : AuthenticationProvider
	{
		public DeviceCodeFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override async ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var response = await InitialiseFlow();
			var tokenResponse = await Poll(response);
			
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
		
		private async Task<DeviceCodeResponse> InitialiseFlow()
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("scope", "user.read openid profile");
			// TODO: Don't hardcode URI
			var resp = await HttpClient.PostAsync("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/devicecode", new FormUrlEncodedContent(dict));
			var respPayload = await resp.Content.ReadAsStringAsync();
			
			var deviceCodeResponse = JsonSerializer.Deserialize<DeviceCodeResponse>(respPayload);
			return deviceCodeResponse;
		}
				
		private async Task<TokenValidResponse> Poll(DeviceCodeResponse deviceCodeResponse)
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("grant_type", "urn:ietf:params:oauth:grant-type:device_code");
			dict.Add("device_code", deviceCodeResponse.DeviceCode);
			
			while (true)
			{
				// TODO: Don't hardcode URI
				var req = new HttpRequestMessage(HttpMethod.Post,
					"https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/token")
				{
					Content = new FormUrlEncodedContent(dict)
				};
				req.Headers.Add("Origin", "localhost");
				
				var resp = await HttpClient.SendAsync(req);
				var respPayload = await resp.Content.ReadAsStringAsync();

				if (resp.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<TokenValidResponse>(respPayload);
				}

				var error = JsonSerializer.Deserialize<TokenErrorResponse>(respPayload);
				if (error.Error.Equals("authorization_pending"))
				{
					//Console.WriteLine($"Sleeping {deviceCodeResponse.Interval}");
				}
				else
				{
					throw new Exception(error.ErrorDescription);
				}

				Thread.Sleep(deviceCodeResponse.Interval * 1000); // Wait the requested interval before polling again.
			}
		}
	}
}
