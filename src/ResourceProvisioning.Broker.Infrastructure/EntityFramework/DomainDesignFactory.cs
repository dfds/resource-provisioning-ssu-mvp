using System;
using System.Data.Common;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework
{
	public class DomainDesignFactory : IDesignTimeDbContextFactory<DomainContext>
	{
		public DomainContext CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<DomainContext>()
				.UseSqlite(CreateDatabaseConnection(config.GetConnectionString(nameof(DomainContext))));

			return new DomainContext(optionsBuilder.Options, new FakeMediator());
		}

		private static DbConnection CreateDatabaseConnection(string connectionString)
		{
			var connection = new SqliteConnection(connectionString);

			connection.Open();

			return connection;
		}
	}
}
