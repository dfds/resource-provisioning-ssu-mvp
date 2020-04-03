using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public class EnvironmentRepository : BaseRepository<DomainDbContext, Domain.Aggregates.EnvironmentAggregate.Environment>, IEnvironmentRepository
	{
		public EnvironmentRepository(DomainDbContext context) : base(context)
		{

		}

		IUnitOfWork IRepository.UnitOfWork => throw new NotImplementedException();

		public override async Task<IEnumerable<Domain.Aggregates.EnvironmentAggregate.Environment>> GetAsync(Expression<Func<Domain.Aggregates.EnvironmentAggregate.Environment, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Environments
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
			var environment = await _context.Environments.FindAsync(contextId);

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
