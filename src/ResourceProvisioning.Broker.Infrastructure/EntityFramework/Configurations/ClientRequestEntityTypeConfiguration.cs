using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
	{
		public void Configure(EntityTypeBuilder<ClientRequest> configuration)
		{
			configuration.ToTable("Request", DomainContext.DEFAULT_SCHEMA);
			configuration.HasKey(cr => cr.Id);
			configuration.Property(cr => cr.Name).IsRequired();
			configuration.Property(cr => cr.Time).IsRequired();
		}
	}
}
