using System.Net.Http;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Net.Http;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http.Response
{
	internal class JsonResponse : HttpResponseMessage
	{
		public new virtual Task<IJsonContent> Content { get; set; }
	}
}
