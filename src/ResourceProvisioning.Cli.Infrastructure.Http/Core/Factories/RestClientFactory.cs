using System;
using System.Net.Http;

namespace ResourceProvisioning.Cli.RestClient.Core.Factories
{
    public static class RestClientFactory
    {
        public static IRestClient CreateFromBaseUri(Uri uri)
        {
            return new ResourceProvisioning.Cli.RestClient.Core.RestClient(new HttpClient{BaseAddress = uri});
        }
    }
}
