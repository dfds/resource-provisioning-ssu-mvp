using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Host.Api.Application.EventHandlers
{
	public class EnvironmentStatusChangedToCancelledDomainEventHandler : INotificationHandler<EnvironmentStatusChangedToCancelledEvent>
	{
		private readonly ILoggerFactory _logger;
		private readonly IEnvironmentRepository _environmentRepository;

		public EnvironmentStatusChangedToCancelledDomainEventHandler(
			ILoggerFactory logger,
			IEnvironmentRepository environmentRepository)
		{
			_environmentRepository = environmentRepository ?? throw new ArgumentNullException(nameof(environmentRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Handle(EnvironmentStatusChangedToCancelledEvent environmentCancelledEvent, CancellationToken cancellationToken)
		{
			var logger = _logger.CreateLogger(nameof(EnvironmentStatusChangedToCancelledDomainEventHandler));
			var environment = await _environmentRepository.GetByIdAsync(environmentCancelledEvent.EnvironmentId);

			//TODO.
		}
	}
}
