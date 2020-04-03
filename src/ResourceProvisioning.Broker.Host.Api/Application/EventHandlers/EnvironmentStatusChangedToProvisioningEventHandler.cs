using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Host.Api.Application.EventHandlers
{
	public class EnvironmentStatusChangedToProvisioningEventHandler : INotificationHandler<EnvironmentStatusChangedToProvisioningEvent>
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

		public async Task Handle(EnvironmentStatusChangedToProvisioningEvent environmentProvisioningEvent, CancellationToken cancellationToken)
		{
			var logger = _logger.CreateLogger(nameof(EnvironmentStatusChangedToProvisioningEvent));
			var environment = await _environmentRepository.GetByIdAsync(environmentProvisioningEvent.EnvironmentId);

			//TODO.
		}
	}
}
