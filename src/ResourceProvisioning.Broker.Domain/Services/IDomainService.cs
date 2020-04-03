using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Service
{
	public interface IDomainService
	{
		Task<DesiredState> GetAsync(Expression<Func<DesiredState, bool>> predicate);

		Task<DesiredState> AddAsync(DesiredState state);

		Task<DesiredState> UpdateAsync(DesiredState state);

		Task<DesiredState> DeleteAsync(DesiredState state);
	}
}
