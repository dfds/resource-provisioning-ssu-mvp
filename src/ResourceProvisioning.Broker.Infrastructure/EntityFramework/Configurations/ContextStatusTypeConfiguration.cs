using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ContextStatusEntityTypeConfiguration : IEntityTypeConfiguration<ContextStatus>
	{
		public void Configure(EntityTypeBuilder<ContextStatus> ContextStatusConfiguration)
		{
			ContextStatusConfiguration.ToTable("ContextStatus", DomainDbContext.DEFAULT_SCHEMA);
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
