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
	public sealed class DeleteEnvironmentCommandHandler : CommandHandler<DeleteEnvironmentCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public DeleteEnvironmentCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IProvisioningResponse> Handle(DeleteEnvironmentCommand command, CancellationToken cancellationToken = default)
		{
			if (command.EnvironmentId != Guid.Empty)
			{
				try
				{
					await _controlPlaneService.DeleteEnvironmentAsync(command.EnvironmentId, cancellationToken);
				}
				catch
				{
					return new ProvisioningBrokerResponse(true);
				}
				
			}
			
			return new ProvisioningBrokerResponse(true);
		}
	}
}
