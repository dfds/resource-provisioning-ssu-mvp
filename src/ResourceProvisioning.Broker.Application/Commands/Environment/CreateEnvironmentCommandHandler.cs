using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Application.Commands.Idempotency;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;
using ResourceProvisioning.Broker.Repository;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	public class CreateEnvironmentCommandHandler : BaseCommandHandler<CreateEnvironmentCommand, bool>
	{
		private readonly IEnvironmentRepository _environmentRepository;

		public CreateEnvironmentCommandHandler(IMediator mediator, IEnvironmentRepository environmentRepository) : base(mediator)
		{
			_environmentRepository = environmentRepository ?? throw new ArgumentNullException(nameof(environmentRepository));
		}

		public override async Task<bool> Handle(CreateEnvironmentCommand command, CancellationToken cancellationToken)
		{
			var desiredState = new DesiredState(command.DesiredState.Option1, command.DesiredState.Option2, command.DesiredState.Option3, command.DesiredState.Option4);
			var environment = new Domain.Aggregates.EnvironmentAggregate.Environment(desiredState);
			
			_environmentRepository.Add(environment);
			
			return await _environmentRepository.UnitOfWork.SaveEntitiesAsync();
		}
	}


	// Use for Idempotency in Command process
	public class CreateEnvironmentIdentifiedCommandHandler : IdentifiedCommandHandler<CreateEnvironmentCommand, bool>
	{
		public CreateEnvironmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
		{
		}

		protected override bool CreateResultForDuplicateRequest()
		{
			// Ignore duplicate requests.
			return true;                
		}
	}
}
