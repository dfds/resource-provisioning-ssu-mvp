using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Infrastructure.Protocols.Http.Content
{
	internal class JsonContent : HttpContent
	{
		public JsonDocument Document { get; }

		public JsonContent()
		{
			Headers.ContentType = new MediaTypeHeaderValue("application/json");
		}

		public JsonContent(string json)
		{
			if (string.IsNullOrEmpty(json)) return;

			Document = JsonDocument.Parse(json);
		}

		public JsonContent(byte[] data)
		{
			if (data.Length <= 0) return;

			Document = JsonDocument.Parse(data);
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			using (var jsonWriter = new Utf8JsonWriter(stream))
			{
				Document.WriteTo(jsonWriter);

				jsonWriter.Flush();
			}

			return Task.CompletedTask;
		}

		protected override bool TryComputeLength(out long length)
		{
			length = 0;

			if (Document == null)
				return true;

			try
			{
				length = JsonSerializer.Serialize(Document).Length;
			}
			catch (JsonException)
			{
				return false;
			}

			return true;
		}
	}
}
