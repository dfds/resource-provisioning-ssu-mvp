using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Facade
{
	public abstract class Facade
	{
		private readonly IMediator _mediator;
		private readonly ILogger<Facade> _logger;

		protected Facade(IMediator mediator, ILogger<Facade> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<T> Execute<T>(ICommand<T> command, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation($"Processing {command.GetType().FullName}");

			return await _mediator.Send(command, cancellationToken);
		}
	}
}
