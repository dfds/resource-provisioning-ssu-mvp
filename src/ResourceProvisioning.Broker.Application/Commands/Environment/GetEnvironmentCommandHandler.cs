using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Protocols.Http;
using ResourceProvisioning.Broker.Domain.Services;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	public sealed class GetEnvironmentCommandHandler : ICommandHandler<GetEnvironmentCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public GetEnvironmentCommandHandler(IControlPlaneService controlPlaneService)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public async Task<IProvisioningResponse> Handle(GetEnvironmentCommand command, CancellationToken cancellationToken = default)
		{
			dynamic result;

			if (command.EnvironmentId != Guid.Empty)
			{
				result = await _controlPlaneService.GetEnvironmentByIdAsync(command.EnvironmentId);
			}
			else 
			{
				result = await _controlPlaneService.GetEnvironmentsAsync();
			}
			
			return new ProvisioningBrokerResponse(result);
		}
	}
}
