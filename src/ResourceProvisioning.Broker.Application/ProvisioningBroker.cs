using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Net.Http;

namespace ResourceProvisioning.Broker.Application
{
	//TODO: Implement application commands & commandhandlers (Ch2139)
	//TODO: Implement integration events & eventhandlers (Ch2139)
	public sealed class ProvisioningBroker : IProvisioningBroker
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public Guid Id => Guid.NewGuid();

		public GridActorType ActorType => GridActorType.System;

		public ProvisioningBroker(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public Task<IProvisioningResponse> Handle(IProvisioningRequest request, CancellationToken cancellationToken)
		{
			//TODO: Map IProvisioningRequest to IProvisioningEvent (Ch2139)
			var provisioningEvent = _mapper.Map<IProvisioningRequest, IProvisioningEvent>(request);

			_mediator.Publish(provisioningEvent, cancellationToken);

			return Task.FromResult<IProvisioningResponse>(new ProvisionBrokerResponse());
		}

		public Task Handle(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			//TODO: Implement simple event handler logic for ProvisioningBroker (Ch2139)
			throw new NotImplementedException();
		}
	}
}
