using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Models;

namespace ResourceProvisioning.Cli.Core.Core.Repositories
{
    public interface IStateRepository
    {
        Task<IEnumerable<ActualState>> GetStatesByIdAsync(
            Guid environmentId
        );
    }
}
