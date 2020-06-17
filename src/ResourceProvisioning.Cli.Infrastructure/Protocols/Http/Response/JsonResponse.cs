using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Infrastructure.Protocols.Http.Response
{
	internal class JsonResponse : HttpResponseMessage
	{
		public new virtual Task<JsonElement> Content { get; set; }
	}
}
