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
			configuration.ToTable("Environment");
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<DateTime>("CreateDate").IsRequired();
			configuration.Property<int>("StatusId").IsRequired();

			configuration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");

			configuration.HasOne(o => o.DesiredState)
				.WithOne()
				.HasForeignKey("DesiredState");

			var resourceNavigation = configuration.Metadata.FindNavigation(nameof(EnvironmentRoot.Resources));

			resourceNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
