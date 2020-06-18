using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public class DeviceCode : IAuthentication
	{
		private HttpClient _httpClient;

		public DeviceCode()
		{
			_httpClient = new HttpClient();
		}

		public async Task<AuthenticationToken> Auth()
		{
			var response = await InitialiseFlow();
			var tokenResponse = await Poll(response);
			
			return new AuthenticationToken
			{
				AccessToken = tokenResponse.AccessToken,
				IdToken = tokenResponse.IdToken,
				Scope = tokenResponse.Scope,
				ExpiresIn = tokenResponse.ExpiresIn,
				TokenType = tokenResponse.TokenType
			};
		}
		
		private async Task<DeviceCodeResponse> InitialiseFlow()
		{
			var dict = new Dictionary<string, string>();
			// TODO: Don't hardcode client_id 
			dict.Add("client_id", "72d0443b-ff34-4568-8eb9-1d81849c5462");
			dict.Add("scope", "user.read openid profile");
			// TODO: Don't hardcode URI
			var resp = await _httpClient.PostAsync("https://login.microsoftonline.com/73a99466-ad05-4221-9f90-e7142aa2f6c1/oauth2/v2.0/devicecode", new FormUrlEncodedContent(dict));
			var respPayload = await resp.Content.ReadAsStringAsync();
			
			var deviceCodeResponse = JsonConvert.DeserializeObject<DeviceCodeResponse>(respPayload);
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
				
				var resp = await _httpClient.SendAsync(req);
				var respPayload = await resp.Content.ReadAsStringAsync();

				if (resp.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<TokenValidResponse>(respPayload);
				}

				var error = JsonConvert.DeserializeObject<TokenErrorResponse>(respPayload);
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
	
	class DeviceCodeResponse
	{
		[JsonProperty("user_code")]
		public string UserCode { get; set; }
		[JsonProperty("device_code")]
		public string DeviceCode { get; set; }
		[JsonProperty("verification_uri")]
		public string VerificationUri { get; set; }
		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
		[JsonProperty("interval")]
		public int Interval { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}

	class TokenValidResponse // 200
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
		[JsonProperty("ext_expires_in")]
		public int ExtExpiresIn { get; set; }
	}

	class TokenErrorResponse // 400
	{
		[JsonProperty("error")]
		public string Error { get; set; }
		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }
		[JsonProperty("error_codes")]
		public IEnumerable<int> ErrorCodes { get; set; }
		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }
		[JsonProperty("trace_id")]
		public string TraceId { get; set; }
		[JsonProperty("correlation_id")]
		public string CorrelationId { get; set; }
		[JsonProperty("error_uri")]
		public string ErrorUri { get; set; }
	}
}
