using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Infrastructure;
using ResourceProvisioning.Abstractions.Infrastructure.Provisioning;

namespace ResourceProvisioning.Broker.Application
{
	public class ProvisioningBroker : IProvisioningBroker
	{
		private readonly IMediator _mediator;
		private readonly IQueryProvider _queryProvider;

		public Guid Id { get; internal set; }

        public GridActorType Type => GridActorType.System;

		public ProvisioningBroker(IMediator mediator, IQueryProvider queryProvider, IOptions<ProvisioningBrokerOptions> options = default) 
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_queryProvider = queryProvider ?? throw new ArgumentNullException(nameof(queryProvider));
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

		public Task HandleAsync(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			//TODO: Query environments affected by EnvironmentResourceCreatedEvent		
			//TODO: Implement UpdateEnvironmentCommand
			//TODO: Send UpdateEnvironmentCommand to associated command handler via mediator.
			//TODO: Emit EnvironmentUpdatedEvents.
			throw new NotImplementedException();
		}
	}
}
