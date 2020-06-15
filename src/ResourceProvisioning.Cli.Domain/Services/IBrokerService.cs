using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Application.Models;

namespace ResourceProvisioning.Cli.Domain.Services
{
    public interface IBrokerService
    {
	    Task<ActualState> GetCurrentStateAsync(CancellationToken cancellationToken = default);

	    Task<ActualState> GetCurrentStateByEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default);

	    Task ApplyDesiredStateAsync(
		    Guid environmentId,
		    DesiredState desiredState,
		    CancellationToken cancellationToken = default
		);
	}
}
