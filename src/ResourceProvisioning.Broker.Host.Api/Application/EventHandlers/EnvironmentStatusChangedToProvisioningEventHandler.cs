using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events.Domain;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Host.Api.Application.EventHandlers
{
	public class EnvironmentStatusChangedToProvisioningEventHandler : IDomainEventHandler<EnvironmentStatusChangedToProvisioningEvent>
	{
		private readonly ILoggerFactory _logger;
		private readonly IEnvironmentRepository _environmentRepository;

		public EnvironmentStatusChangedToProvisioningEventHandler(
			ILoggerFactory logger,
			IEnvironmentRepository environmentRepository)
		{
			_environmentRepository = environmentRepository ?? throw new ArgumentNullException(nameof(environmentRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task HandleAsync(EnvironmentStatusChangedToProvisioningEvent environmentProvisioningEvent, CancellationToken cancellationToken)
		{
			var logger = _logger.CreateLogger(nameof(EnvironmentStatusChangedToProvisioningEvent));
			var environment = await _environmentRepository.GetByIdAsync(environmentProvisioningEvent.EnvironmentId);

			//TODO: Implement event handler logic
		}
	}
}
