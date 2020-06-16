using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentRootEntityTypeConfiguration : IEntityTypeConfiguration<EnvironmentRoot>
	{
		public void Configure(EntityTypeBuilder<EnvironmentRoot> configuration)
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

			var navigation = configuration.Metadata.FindNavigation(nameof(EnvironmentRoot.Resources));

			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
