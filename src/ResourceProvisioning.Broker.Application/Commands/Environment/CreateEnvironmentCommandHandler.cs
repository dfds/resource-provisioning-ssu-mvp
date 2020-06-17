using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Protocols.Http;
using ResourceProvisioning.Broker.Domain.Services;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	public sealed class CreateEnvironmentCommandHandler : CommandHandler<CreateEnvironmentCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public CreateEnvironmentCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IProvisioningResponse> Handle(CreateEnvironmentCommand command, CancellationToken cancellationToken = default)
		{
			var aggregate = await _controlPlaneService.AddEnvironmentAsync(command.DesiredState, cancellationToken);

			return new ProvisioningBrokerResponse(aggregate);
		}
	}
}
