using System.Text.Json.Serialization;

namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	public partial class DeviceCodeFlow : AuthenticationFlow
	{
		class DeviceCodeResponse
		{
			[JsonPropertyName("user_code")]
			public string UserCode { get; set; }

			[JsonPropertyName("device_code")]
			public string DeviceCode { get; set; }

			[JsonPropertyName("verification_uri")]
			public string VerificationUri { get; set; }

			[JsonPropertyName("expires_in")]
			public int ExpiresIn { get; set; }

			[JsonPropertyName("interval")]
			public int Interval { get; set; }

			[JsonPropertyName("message")]
			public string Message { get; set; }
		}
	}
}
