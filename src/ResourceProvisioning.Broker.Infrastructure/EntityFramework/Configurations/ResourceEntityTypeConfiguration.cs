using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ResourceEntityTypeConfiguration : IEntityTypeConfiguration<Resource>
	{
		public void Configure(EntityTypeBuilder<Resource> ContextItemConfiguration)
		{
			ContextItemConfiguration.ToTable("Resources", DomainDbContext.DEFAULT_SCHEMA);
			ContextItemConfiguration.HasKey(o => o.Id);
			ContextItemConfiguration.Ignore(b => b.DomainEvents);
			ContextItemConfiguration.Property<Guid>("EnvironmentId").IsRequired();
			ContextItemConfiguration.Property<Guid>("ResourceId").IsRequired();
			ContextItemConfiguration.Property<string>("Comment").IsRequired();
		}
	}
}
