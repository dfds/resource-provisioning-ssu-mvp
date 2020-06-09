using RestClient.Core;

namespace ResourceProvisioning.Cli.RestClient.Core
{
    public interface IRestClient
    {
        IStateClient State { get; }
    }
}
