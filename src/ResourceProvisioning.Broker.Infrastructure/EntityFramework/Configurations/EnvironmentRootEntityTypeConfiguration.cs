using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentRootEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Environment.EnvironmentRoot>
	{
		public void Configure(EntityTypeBuilder<Domain.Aggregates.Environment.EnvironmentRoot> configuration)
		{
			configuration.ToTable("Environment", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<DateTime>("CreateDate").IsRequired();
			configuration.Property<int>("StatusId").IsRequired();

			configuration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");

			configuration.OwnsOne(o => o.DesiredState);

			var navigation = configuration.Metadata.FindNavigation(nameof(Domain.Aggregates.Environment.EnvironmentRoot.Resources));

			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
