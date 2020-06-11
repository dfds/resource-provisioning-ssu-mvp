using System;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.RestShared.DataTransferObjects;

namespace ResourceProvisioning.Cli.RestClient.Core
{
    public interface IStateClient
    {
        Task<EnvironmentActualState> GetCurrentStateAsync();
        Task<EnvironmentActualState> GetCurrentStateByEnvironmentAsync(Guid environmentId);
        Task SubmitDesiredStateAsync(
            Guid environmentId,
            DesiredState desiredState
        );
    }
}
