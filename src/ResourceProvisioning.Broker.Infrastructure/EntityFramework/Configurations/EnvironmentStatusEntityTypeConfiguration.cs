using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class EnvironmentStatusEntityTypeConfiguration : IEntityTypeConfiguration<EnvironmentStatus>
	{
		public void Configure(EntityTypeBuilder<EnvironmentStatus> configuration)
		{
			configuration.ToTable("EnvironmentStatus", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(o => o.Id);

			configuration.Property(o => o.Id)
				.HasDefaultValue(1)
				.ValueGeneratedNever()
				.IsRequired();

			configuration.Property(o => o.Name)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}
