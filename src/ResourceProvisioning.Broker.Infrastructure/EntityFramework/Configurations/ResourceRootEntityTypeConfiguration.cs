using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ResourceRootEntityTypeConfiguration : IEntityTypeConfiguration<ResourceRoot>
	{
		public void Configure(EntityTypeBuilder<ResourceRoot> configuration)
		{
			configuration.ToTable("Resource", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<DateTime>("RegisteredDate").IsRequired();
			configuration.Property<int>("StatusId").IsRequired();

			configuration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");

			configuration.OwnsOne(o => o.DesiredState);

			//TODO: Configure cascading delete of related EnvironmentResourceReference entries.
		}
	}
}
