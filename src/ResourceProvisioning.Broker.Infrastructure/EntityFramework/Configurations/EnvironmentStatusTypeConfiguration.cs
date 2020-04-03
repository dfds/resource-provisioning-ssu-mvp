using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentStatusTypeConfiguration : IEntityTypeConfiguration<EnvironmentStatus>
	{
		public void Configure(EntityTypeBuilder<EnvironmentStatus> ContextStatusConfiguration)
		{
			ContextStatusConfiguration.ToTable("EnvironmentStatus", DomainContext.DEFAULT_SCHEMA);
			ContextStatusConfiguration.HasKey(o => o.Id);

			ContextStatusConfiguration.Property(o => o.Id)
				.HasDefaultValue(1)
				.ValueGeneratedNever()
				.IsRequired();

			ContextStatusConfiguration.Property(o => o.Name)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}
