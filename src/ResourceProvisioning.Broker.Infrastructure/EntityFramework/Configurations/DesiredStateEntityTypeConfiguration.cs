using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class DesiredStateEntityTypeConfiguration : IEntityTypeConfiguration<DesiredState>
	{
		public void Configure(EntityTypeBuilder<DesiredState> configuration)
		{
			configuration.ToTable("State");
			configuration.HasKey(o => o.Name);
			configuration.Property(o => o.Name).ValueGeneratedNever();
			configuration.OwnsMany(e => e.Labels)
				         .HasKey("Name");
			configuration.OwnsMany(e => e.Properties)
						 .HasKey("Key");
		}
	}
}
