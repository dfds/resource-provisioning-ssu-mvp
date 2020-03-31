using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;
using ResourceProvisioning.Broker.Infrastructure.Extensions;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework
{
	public class DomainDbContext : DbContext, IUnitOfWork
	{
		public const string DEFAULT_SCHEMA = nameof(DomainDbContext);
		private readonly IMediator _mediator;

		public virtual DbSet<Context> Contexts { get; set; }

		public virtual DbSet<ContextResource> Resources { get; set; }

		public virtual DbSet<ContextStatus> ContextStatus { get; set; }

		public IDbContextTransaction GetCurrentTransaction { get; private set; }

		public DomainDbContext() : this(new DbContextOptions<DomainDbContext>(){}, null) { }
		
		public DomainDbContext(DbContextOptions<DomainDbContext> options, IMediator mediator) : base(options)
		{
			_mediator = mediator;

			System.Diagnostics.Debug.WriteLine($"{nameof(DomainDbContext)}::ctor ->" + GetHashCode());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
			//modelBuilder.ApplyConfiguration(new BaseEmployeeEntityTypeConfiguration());
			//modelBuilder.ApplyConfiguration(new ContextEntityTypeConfiguration());
			//modelBuilder.ApplyConfiguration(new ContextDetailEntityTypeConfiguration());
			//modelBuilder.ApplyConfiguration(new ContextStatusEntityTypeConfiguration());
		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			// Dispatch Domain Events collection. 
			// Choices:
			// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
			// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
			// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
			// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
			await _mediator?.DispatchDomainEventsAsync(this);

			// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
			// performed through the DbContext will be committed
			var result = await base.SaveChangesAsync();

			return true;
		}

		public async Task BeginTransactionAsync()
		{
			GetCurrentTransaction = GetCurrentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await SaveChangesAsync();

				GetCurrentTransaction?.Commit();
			}
			catch
			{
				RollbackTransaction();

				throw;
			}
			finally
			{
				if (GetCurrentTransaction != null)
				{
					GetCurrentTransaction.Dispose();

					GetCurrentTransaction = null;
				}
			}
		}

		public void RollbackTransaction()
		{
			try
			{
				GetCurrentTransaction?.Rollback();
			}
			finally
			{
				if (GetCurrentTransaction != null)
				{
					GetCurrentTransaction.Dispose();

					GetCurrentTransaction = null;
				}
			}
		}
	}
}
