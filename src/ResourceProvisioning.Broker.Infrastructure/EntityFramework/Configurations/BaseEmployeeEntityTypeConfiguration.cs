using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceProvisioning.Broker.Domain.Aggregates;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework.Configurations
{
	class BaseEmployeeEntityTypeConfiguration : IEntityTypeConfiguration<BaseEmployee>
	{
		public void Configure(EntityTypeBuilder<BaseEmployee> employeeConfiguration)
		{
			employeeConfiguration.ToTable("employees", DomainDbContext.DEFAULT_SCHEMA);
			employeeConfiguration.HasKey(m => m.Id);
			employeeConfiguration.Property(m => m.Name).IsRequired();
			employeeConfiguration.Property(m => m.Email).IsRequired();
		}
	}
}
