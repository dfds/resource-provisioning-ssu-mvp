using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class EnvironmentRepository : BaseRepository<DomainContext, Domain.Aggregates.EnvironmentAggregate.Environment>, IEnvironmentRepository
	{
		public EnvironmentRepository(DomainContext context) : base(context)
		{

		}

		public override async Task<IEnumerable<Domain.Aggregates.EnvironmentAggregate.Environment>> GetAsync(Expression<Func<Domain.Aggregates.EnvironmentAggregate.Environment, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Environment
							 .AsNoTracking()
							 .Where(filter)
							 .Include(i => i.Resources)
							 .Include(i => i.Status)
							 .Include(i => i.State)
							 .AsEnumerable();
			});
		}

		public async Task<Domain.Aggregates.EnvironmentAggregate.Environment> GetByIdAsync(Guid contextId)
		{
			var environment = await _context.Environment.FindAsync(contextId);

			if (environment != null)
			{
				var entry = _context.Entry(environment);

				if (entry != null)
				{
					await entry.Collection(i => i.Resources).LoadAsync();
					await entry.Reference(i => i.Status).LoadAsync();
					await entry.Reference(i => i.State).LoadAsync();
				}
			}

			return environment;
		}
	}
}
