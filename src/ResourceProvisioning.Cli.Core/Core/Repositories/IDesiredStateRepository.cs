using System;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Models;

namespace ResourceProvisioning.Cli.Core.Core.Repositories
{
    public interface IDesiredStateRepository
    {
        Task StoreDesiredStateAsync(
            Guid environmentId,
            DesiredState desiredState
        );
    }
}
