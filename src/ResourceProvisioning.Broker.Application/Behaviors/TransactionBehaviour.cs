using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Application.Behaviors
{
	public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
		private readonly DomainContext _dbContext;

		public TransactionBehaviour(DomainContext dbContext,
			ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
		{
			_dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
			_logger = logger ?? throw new ArgumentException(nameof(ILogger));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var response = default(TResponse);

			try
			{
				var strategy = _dbContext.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					_logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

					await _dbContext.BeginTransactionAsync();

					response = await next();

					await _dbContext.CommitTransactionAsync();

					_logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
				});

				return response;
			}
			catch (Exception)
			{
				_logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

				_dbContext.RollbackTransaction();

				throw;
			}
		}
	}
}
