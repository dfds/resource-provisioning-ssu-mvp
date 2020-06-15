using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ProvisioningBroker : IProvisioningBroker
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public Guid Id => Guid.NewGuid();

		public GridActorType Type => GridActorType.System;

		public ProvisioningBroker(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public Task<IProvisioningResponse> Handle(IProvisioningRequest request, CancellationToken cancellationToken)
		{
			var provisioningEvent = _mapper.Map<IProvisioningRequest, IProvisioningEvent>(request);

			_mediator.Publish(provisioningEvent, cancellationToken);

			//TODO: Implement ProvisioningRequest (Action-result'ish)
			throw new NotImplementedException();
		}

		public Task Handle(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			//TODO: Process events.
			throw new NotImplementedException();
		}
	}
}
