using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Application.Commands.Idempotency;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	public class CreateEnvironmentCommandHandler : CommandHandler<CreateEnvironmentCommand, EnvironmentRoot>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public CreateEnvironmentCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<EnvironmentRoot> Handle(CreateEnvironmentCommand command, CancellationToken cancellationToken)
		{						
			return await _controlPlaneService.AddEnvironmentAsync(command.DesiredState);
		}
	}


	//TODO: Finalize Idempotency concept
	public class CreateEnvironmentIdentifiedCommandHandler : IdentifiedCommandHandler<CreateEnvironmentCommand, EnvironmentRoot>
	{
		public CreateEnvironmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
		{
		}

		protected override EnvironmentRoot CreateResultForDuplicateRequest()
		{
			return null;               
		}
	}
}
