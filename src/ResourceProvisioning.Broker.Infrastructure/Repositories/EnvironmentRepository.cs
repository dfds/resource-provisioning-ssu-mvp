using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Domain.Repository;
using ResourceProvisioning.Abstractions.Repositories;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class EnvironmentRepository : Repository<DomainContext, Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot>, IEnvironmentRepository
	{
		public EnvironmentRepository(DomainContext context) : base(context)
		{

		}

		public override async Task<IEnumerable<Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot>> GetAsync(Expression<Func<Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Environment
							 .AsNoTracking()
							 .Where(filter)
							 .Include(i => i.Resources)
							 .Include(i => i.Status)
							 .Include(i => i.DesiredState)
							 .AsEnumerable();
			});
		}

		public async Task<Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot> GetByIdAsync(Guid contextId)
		{
			var environment = await _context.Environment.FindAsync(contextId);

			if (environment != null)
			{
				var entry = _context.Entry(environment);

				if (entry != null)
				{
					await entry.Collection(i => i.Resources).LoadAsync();
					await entry.Reference(i => i.Status).LoadAsync();
					await entry.Reference(i => i.DesiredState).LoadAsync();
				}
			}

			return environment;
		}

		public override Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot Add(Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public override Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot Update(Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public override void Delete(Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}
	}
}
