using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Application.Infrastructure.EntityFramework
{
	public class DomainContextSeeder
	{
		public static async Task SeedAsync(DomainContext context, ILogger<DomainContextSeeder> logger = default)
		{
			var policy = CreatePolicy(nameof(DomainContextSeeder), logger);

			await policy.ExecuteAsync(async () =>
			{
				logger?.LogInformation("Seeding database.");

				await using (context)
				{
					context.Status.AddRange(GridActorStatus.List());

					await context.SaveChangesAsync();
				}

				logger?.LogInformation("Done seeding database.");
			});
		}

		private static AsyncRetryPolicy CreatePolicy(string prefix, ILogger logger = default, int retries = 3)
		{
			return Policy.Handle<SqliteException>().
				WaitAndRetryAsync(
					retryCount: retries,
					sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						logger?.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
					}
				);
		}
	}
}
