using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Domain.Services;

namespace ResourceProvisioning.Broker.Application
{
	public class ProvisioningBroker : IProvisioningBroker
	{
		private readonly IMediator _mediator;
		private readonly IControlPlaneService _controlPlaneService;
		private readonly ProvisioningBrokerOptions _options;

		public Guid Id { get; internal set; }

        public GridActorType Type => GridActorType.System;

		public IDesiredState DesiredState => throw new NotImplementedException();

		public ProvisioningBroker(IMediator mediator, IControlPlaneService controlPlaneService, IOptions<ProvisioningBrokerOptions> options = default) 
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
			_options = options?.Value;
		}

		public Task<IProvisioningResponse> Handle(IProvisioningRequest request, CancellationToken cancellationToken)
		{
			//TODO: Map request to event.
			//TODO: Implement provisioning event.
			IProvisioningEvent provisioningEvent = null;

			_mediator.Publish(provisioningEvent, cancellationToken);

			//TODO: Decide expected result for broker.
			throw new NotImplementedException();
		}

		public Task Handle(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			//TODO: Query environments affected by EnvironmentResourceCreatedEvent		
			//TODO: Implement UpdateEnvironmentCommand
			//TODO: Send UpdateEnvironmentCommand to associated command handler via mediator.
			//TODO: Emit EnvironmentUpdatedEvents.
			throw new NotImplementedException();
		}
	}
}
