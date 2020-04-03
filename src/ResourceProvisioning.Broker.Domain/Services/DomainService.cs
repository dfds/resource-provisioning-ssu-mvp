using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Repository;
using ResourceProvisioning.Broker.Service;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public class DomainService : IDomainService
	{
		private readonly IEnvironmentRepository _environmentRepository;

		public DomainService(IEnvironmentRepository environmentRepository)
		{
			_environmentRepository = environmentRepository;
		}

		public Task<DesiredState> AddAsync(DesiredState state)
		{
			throw new NotImplementedException();
		}

		public Task<DesiredState> DeleteAsync(DesiredState state)
		{
			throw new NotImplementedException();
		}

		public Task<DesiredState> GetAsync(Expression<Func<DesiredState, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<DesiredState> GetByContextId(Guid contextId)
		{
			throw new NotImplementedException();
		}

		public Task<DesiredState> UpdateAsync(DesiredState state)
		{
			throw new NotImplementedException();
		}
	}
}
