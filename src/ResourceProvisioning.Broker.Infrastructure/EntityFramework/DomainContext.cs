using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework
{
	public class DomainContext : DbContext, IUnitOfWork
	{
		public const string DEFAULT_SCHEMA = nameof(DomainContext);
		private readonly IMediator _mediator;

		public virtual DbSet<EnvironmentRoot> Environment { get; set; }

		public virtual DbSet<ResourceRoot> Resource { get; set; }

		public IDbContextTransaction GetCurrentTransaction { get; private set; }

		public DomainContext() : this(new DbContextOptions<DomainContext>(){}, null) { }
		
		public DomainContext(DbContextOptions<DomainContext> options, IMediator mediator) : base(options)
		{
			_mediator = mediator;

			System.Diagnostics.Debug.WriteLine($"{nameof(DomainContext)}::ctor ->" + GetHashCode());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new GridActorStatusEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new ResourceRootEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EnvironmentRootEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EnvironmentResourceReferenceEntityTypeConfiguration());
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
