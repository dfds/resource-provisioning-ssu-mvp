using System.Net.Http;

namespace ResourceProvisioning.Cli.RestClient.Core
{
    public class RestClient : IRestClient
    {
        public IStateClient State { get; }

        public RestClient(HttpClient httpClient)
        {
            State = new StateClient(httpClient);
        }
    }
}
