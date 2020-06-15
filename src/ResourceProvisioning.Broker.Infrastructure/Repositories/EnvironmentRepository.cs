using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Repository;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class EnvironmentRepository : Repository<DomainContext, Domain.Aggregates.Environment.EnvironmentRoot>, IEnvironmentRepository
	{
		public EnvironmentRepository(DomainContext context) : base(context)
		{

		}

		public override async Task<IEnumerable<Domain.Aggregates.Environment.EnvironmentRoot>> GetAsync(Expression<Func<Domain.Aggregates.Environment.EnvironmentRoot, bool>> filter)
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

		public async Task<Domain.Aggregates.Environment.EnvironmentRoot> GetByIdAsync(Guid contextId)
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

		public override Domain.Aggregates.Environment.EnvironmentRoot Add(Domain.Aggregates.Environment.EnvironmentRoot aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public override Domain.Aggregates.Environment.EnvironmentRoot Update(Domain.Aggregates.Environment.EnvironmentRoot aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public override void Delete(Domain.Aggregates.Environment.EnvironmentRoot aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}
	}
}
