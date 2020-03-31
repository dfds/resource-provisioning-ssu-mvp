using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ContextEntityTypeConfiguration : IEntityTypeConfiguration<Context>
	{
		public void Configure(EntityTypeBuilder<Context> ContextConfiguration)
		{
			ContextConfiguration.ToTable("Contexts", DomainDbContext.DEFAULT_SCHEMA);
			ContextConfiguration.HasKey(o => o.Id);
			ContextConfiguration.Ignore(b => b.DomainEvents);
			ContextConfiguration.Property<DateTime>("CreateDate").IsRequired();
			ContextConfiguration.Property<Guid?>("OwnerId").IsRequired(false);
			ContextConfiguration.Property<int>("ContextStatusId").IsRequired();
			ContextConfiguration.Property<string>("Description").IsRequired(false);

			//DesiredState value object persisted as owned entity type supported since EF Core 2.0
			ContextConfiguration.OwnsOne(o => o.TargetState);

			var navigation = ContextConfiguration.Metadata.FindNavigation(nameof(Context.ContextResources));

			// DDD Patterns comment:
			//Set as field (New since EF 1.1) to access the ContextDetails collection property through its field
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			ContextConfiguration.HasOne<BaseEmployee>()
				.WithMany()
				.IsRequired(false)
				.HasForeignKey("OwnerId")
				.OnDelete(DeleteBehavior.SetNull);

			ContextConfiguration.HasOne(o => o.ContextStatus)
				.WithMany()
				.HasForeignKey("ContextStatusId");
		}
	}
}
