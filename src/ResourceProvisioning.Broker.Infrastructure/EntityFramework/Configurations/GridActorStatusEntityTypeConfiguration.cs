using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class GridActorStatusEntityTypeConfiguration : IEntityTypeConfiguration<GridActorStatus>
	{
		public void Configure(EntityTypeBuilder<GridActorStatus> configuration)
		{
			configuration.ToTable("Status");
			configuration.HasKey(o => o.Id);

			configuration.Property(o => o.Id)
				.ValueGeneratedNever()
				.IsRequired();

			configuration.Property(o => o.Name)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}
