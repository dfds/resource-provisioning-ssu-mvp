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
		private readonly IDomainService _domainService;
		private readonly ProvisioningBrokerOptions _options;

		public Guid Id { get; internal set; }

        public GridActorType Type => GridActorType.System;

		public ProvisioningBroker(IMediator mediator, IDomainService domainService, IOptions<ProvisioningBrokerOptions> options = default) 
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
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
