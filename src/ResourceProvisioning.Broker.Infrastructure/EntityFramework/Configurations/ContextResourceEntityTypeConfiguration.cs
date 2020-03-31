using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ContextResourceEntityTypeConfiguration : IEntityTypeConfiguration<Resource>
	{
		public void Configure(EntityTypeBuilder<Resource> ContextItemConfiguration)
		{
			ContextItemConfiguration.ToTable("ContextDetails", DomainDbContext.DEFAULT_SCHEMA);
			ContextItemConfiguration.HasKey(o => o.Id);
			ContextItemConfiguration.Ignore(b => b.DomainEvents);
			ContextItemConfiguration.Property<Guid>("ContextId").IsRequired();
			ContextItemConfiguration.Property<Guid>("ResourceId").IsRequired();
			ContextItemConfiguration.Property<string>("Comment").IsRequired();
		}
	}
}
