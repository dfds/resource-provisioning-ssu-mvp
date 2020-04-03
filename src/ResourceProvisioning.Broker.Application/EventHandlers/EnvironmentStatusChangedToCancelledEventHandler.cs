using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events.Domain;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Application.EventHandlers
{
	public class EnvironmentStatusChangedToCancelledDomainEventHandler : IDomainEventHandler<EnvironmentStatusChangedToCancelledEvent>
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

		public async Task HandleAsync(EnvironmentStatusChangedToCancelledEvent environmentCancelledEvent, CancellationToken cancellationToken)
		{
			var logger = _logger.CreateLogger(nameof(EnvironmentStatusChangedToCancelledDomainEventHandler));
			var environment = await _environmentRepository.GetByIdAsync(environmentCancelledEvent.EnvironmentId);

			//TODO: Implement event handler logic
		}
	}
}
