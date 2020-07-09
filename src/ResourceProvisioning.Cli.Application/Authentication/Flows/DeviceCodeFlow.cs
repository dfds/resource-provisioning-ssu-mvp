using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	public partial class DeviceCodeFlow : AuthenticationFlow
	{
		public DeviceCodeFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override async ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var response = await InitialiseFlow();
			var tokenResponse = await Poll(response);
			
			return new JwtSecurityToken(tokenResponse.IdToken);
		}
		
		private async Task<DeviceCodeResponse> InitialiseFlow()
		{
			var dict = new Dictionary<string, string>();
			dict.Add("client_id", CliApplicationOptions.Authentication.ClientId);
			dict.Add("scope", "user.read openid profile");

			var resp = await HttpClient.PostAsync($"{CliApplicationOptions.Authentication.Instance}/{CliApplicationOptions.Authentication.TenantId}/oauth2/v2.0/devicecode", new FormUrlEncodedContent(dict));
			var respPayload = await resp.Content.ReadAsStringAsync();
			
			var deviceCodeResponse = JsonSerializer.Deserialize<DeviceCodeResponse>(respPayload);
			return deviceCodeResponse;
		}
				
		private async Task<TokenValidResponse> Poll(DeviceCodeResponse deviceCodeResponse)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("client_id", CliApplicationOptions.Authentication.ClientId);
			dict.Add("grant_type", "urn:ietf:params:oauth:grant-type:device_code");
			dict.Add("device_code", deviceCodeResponse.DeviceCode);
			
			while (true)
			{
				var req = new HttpRequestMessage(HttpMethod.Post,
					$"{CliApplicationOptions.Authentication.Instance}/{CliApplicationOptions.Authentication.TenantId}/oauth2/v2.0/token")
				{
					Content = new FormUrlEncodedContent(dict)
				};

				req.Headers.Add("Origin", CliApplicationOptions.Authentication.HostName);
				
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
