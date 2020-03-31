using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Repositories;

namespace ResourceProvisioning.Broker.Infrastructure.Repositories
{
	public abstract class BaseRepository<TContext, TAggregate> : IRepository<TAggregate> 
		where TContext : DbContext, IUnitOfWork 
		where TAggregate : class, IAggregateRoot
	{
		protected readonly TContext _context;

		public IUnitOfWork UnitOfWork
		{
			get
			{
				return _context;
			}
		}

		protected BaseRepository(TContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public virtual TAggregate Add(TAggregate aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public virtual TAggregate Update(TAggregate aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public virtual void Delete(TAggregate aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}

		public abstract Task<IEnumerable<TAggregate>> GetAsync(Expression<Func<TAggregate, bool>> filter);
	}
}
