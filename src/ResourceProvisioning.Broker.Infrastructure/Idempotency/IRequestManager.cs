using System;
using System.Threading.Tasks;

namespace ResourceProvisioning.Broker.Infrastructure.Idempotency
{
	public interface IRequestManager
	{
		Task<bool> ExistAsync(Guid id);

		Task CreateRequestForCommandAsync<T>(Guid id);
	}
}
