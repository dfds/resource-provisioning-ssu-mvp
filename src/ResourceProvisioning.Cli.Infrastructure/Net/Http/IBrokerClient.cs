using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Infrastructure.Net.Http
{
    public interface IBrokerClient
    {
	    Task<dynamic> GetCurrentStateAsync(CancellationToken cancellationToken = default);

	    Task<dynamic> GetCurrentStateByEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default);

	    Task ApplyDesiredStateAsync(
		    Guid environmentId,
		    dynamic desiredState,
		    CancellationToken cancellationToken = default
		);
	}
}
