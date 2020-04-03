using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Infrastructure.EntityFramework
{
	public class ContextProcessingDesignFactory : IDesignTimeDbContextFactory<DomainContext>
	{
		public DomainContext CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<DomainContext>()
				.UseSqlite(config.GetConnectionString(nameof(DomainContext)));

			return new DomainContext(optionsBuilder.Options, new NoMediator());
		}

		class NoMediator : IMediator
		{
			public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
			{
				return Task.CompletedTask;
			}

			public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
			{
				throw new NotImplementedException();
			}

			public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.FromResult(default(TResponse));
			}

			public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.CompletedTask;
			}

			public Task<object> Send(object request, CancellationToken cancellationToken = default)
			{
				return Task.FromResult(default(object));
			}
		}
	}
}
