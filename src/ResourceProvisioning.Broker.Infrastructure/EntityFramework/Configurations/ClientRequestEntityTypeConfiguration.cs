using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
	{
		public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
		{
			requestConfiguration.ToTable("requests", DomainContext.DEFAULT_SCHEMA);
			requestConfiguration.HasKey(cr => cr.Id);
			requestConfiguration.Property(cr => cr.Name).IsRequired();
			requestConfiguration.Property(cr => cr.Time).IsRequired();
		}
	}
}
