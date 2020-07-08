using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	class TokenErrorResponse
	{
		HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

		[JsonPropertyName("error")]
		public string Error { get; set; }

		[JsonPropertyName("error_description")]
		public string ErrorDescription { get; set; }

		[JsonPropertyName("error_codes")]
		public IEnumerable<int> ErrorCodes { get; set; }

		[JsonPropertyName("timestamp")]
		public string Timestamp { get; set; }

		[JsonPropertyName("trace_id")]
		public string TraceId { get; set; }

		[JsonPropertyName("correlation_id")]
		public string CorrelationId { get; set; }

		[JsonPropertyName("error_uri")]
		public string ErrorUri { get; set; }
	}
}
