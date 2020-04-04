using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentResourceEntityTypeConfiguration : IEntityTypeConfiguration<EnvironmentResource>
	{
		public void Configure(EntityTypeBuilder<EnvironmentResource> configuration)
		{
			configuration.ToTable("EnvironmentResource", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<Guid>("EnvironmentId").IsRequired();
			configuration.Property<DateTime>("Provisioned").IsRequired();
			configuration.Property<bool>("IsDesired").IsRequired();
			configuration.Property<string>("Comment").IsRequired();
		}
	}
}
