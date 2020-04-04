using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentResourceReferenceEntityTypeConfiguration : IEntityTypeConfiguration<EnvironmentResourceReference>
	{
		public void Configure(EntityTypeBuilder<EnvironmentResourceReference> configuration)
		{
			configuration.ToTable("EnvironmentResourceReference", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<Guid>("EnvironmentId").IsRequired();
			configuration.Property<Guid>("ResourceId").IsRequired();
			configuration.Property<DateTime>("Provisioned").IsRequired();
			configuration.Property<string>("Comment").IsRequired();
		}
	}
}
