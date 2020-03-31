using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class ContextRepository : BaseRepository<DomainDbContext, Context>, IContextRepository
	{
		public ContextRepository(DomainDbContext context) : base(context)
		{

		}

		IUnitOfWork IRepository<Context>.UnitOfWork => throw new NotImplementedException();

		public override async Task<IEnumerable<Context>> GetAsync(Expression<Func<Context, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Contexts
							 .AsNoTracking()
							 .Where(filter)
							 .Include(i => i.ContextResources)
							 .Include(i => i.ContextStatus)
							 .Include(i => i.TargetState)
							 .AsEnumerable();
			});
		}

		public async Task<Context> GetByIdAsync(Guid ContextId)
		{
			var Context = await _context.Contexts.FindAsync(ContextId);

			if (Context != null)
			{
				var entry = _context.Entry(Context);

				if (entry != null)
				{
					await entry.Collection(i => i.ContextResources).LoadAsync();
					await entry.Reference(i => i.ContextStatus).LoadAsync();
					await entry.Reference(i => i.TargetState).LoadAsync();
				}
			}

			return Context;
		}
	}
}
