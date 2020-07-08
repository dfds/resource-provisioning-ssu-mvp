﻿using System.Net;
using System.Text.Json.Serialization;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	class TokenValidResponse
	{
		HttpStatusCode StatusCode => HttpStatusCode.OK;

		[JsonPropertyName("token_type")]
		public string TokenType { get; set; }

		[JsonPropertyName("scope")]
		public string Scope { get; set; }

		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }

		[JsonPropertyName("id_token")]
		public string IdToken { get; set; }
		
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonPropertyName("ext_expires_in")]
		public int ExtExpiresIn { get; set; }
	}
}
