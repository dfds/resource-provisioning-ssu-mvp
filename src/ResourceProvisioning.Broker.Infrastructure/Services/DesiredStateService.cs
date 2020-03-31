using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;
using ResourceProvisioning.Broker.Repository;
using ResourceProvisioning.Broker.Service;

namespace ResourceProvisioning.Broker.Infrastructure.Services
{
	public class DesiredStateService : IDesiredStateService
	{
		private readonly IContextRepository _contextRepository;

		public DesiredStateService(IContextRepository contextRepository)
		{
			_contextRepository = contextRepository;
		}

		public Task<DesiredState> GetByContextId(Guid contextId)
		{
			throw new NotImplementedException();
		}
	}
}
