using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;
using ResourceProvisioning.Broker.Domain.Repository;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class ResourceRepository : Repository<DomainContext, ResourceRoot>, IResourceRepository
	{
		public ResourceRepository(DomainContext context) : base(context)
		{

		}

		public override async Task<IEnumerable<ResourceRoot>> GetAsync(Expression<Func<ResourceRoot, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Resource
							 .AsNoTracking()
							 .Where(filter)
							 .Include(i => i.Status)
							 .AsEnumerable();
			});
		}

		public async Task<ResourceRoot> GetByIdAsync(Guid contextId)
		{
			var resource = await _context.Resource.FindAsync(contextId);

			if (resource != null)
			{
				var entry = _context.Entry(resource);

				if (entry != null)
				{
					await entry.Reference(i => i.Status).LoadAsync();
				}
			}

			return resource;
		}

		public override ResourceRoot Add(ResourceRoot aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public override ResourceRoot Update(ResourceRoot aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public override void Delete(ResourceRoot aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}
	}
}
