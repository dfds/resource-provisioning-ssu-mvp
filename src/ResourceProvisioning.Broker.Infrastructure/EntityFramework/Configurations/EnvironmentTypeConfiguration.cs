using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.EnvironmentAggregate.Environment>
	{
		public void Configure(EntityTypeBuilder<Domain.Aggregates.EnvironmentAggregate.Environment> contextConfiguration)
		{
			contextConfiguration.ToTable("Environments", DomainDbContext.DEFAULT_SCHEMA);
			contextConfiguration.HasKey(o => o.Id);
			contextConfiguration.Ignore(b => b.DomainEvents);
			contextConfiguration.Property<DateTime>("CreateDate").IsRequired();
			contextConfiguration.Property<Guid?>("OwnerId").IsRequired(false);
			contextConfiguration.Property<int>("StatusId").IsRequired();
			contextConfiguration.Property<string>("Description").IsRequired(false);

			//DesiredState value object persisted as owned entity type supported since EF Core 2.0
			contextConfiguration.OwnsOne(o => o.State);

			var navigation = contextConfiguration.Metadata.FindNavigation(nameof(Domain.Aggregates.EnvironmentAggregate.Environment.Resources));

			// DDD Patterns comment:
			//Set as field (New since EF 1.1) to access the Environment.Resource collection property through its field
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			contextConfiguration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");
		}
	}
}
