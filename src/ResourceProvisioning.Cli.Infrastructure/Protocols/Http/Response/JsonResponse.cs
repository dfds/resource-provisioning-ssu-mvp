using System.Net.Http;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Protocols.Http;

namespace ResourceProvisioning.Cli.Infrastructure.Protocols.Http.Response
{
	internal class JsonResponse : HttpResponseMessage
	{
		public new virtual Task<IJsonContent> Content { get; set; }
	}
}
