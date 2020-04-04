using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Application.Commands.Idempotency;
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	public class CreateEnvironmentCommandHandler : BaseCommandHandler<CreateEnvironmentCommand, Domain.Aggregates.EnvironmentAggregate.Environment>
	{
		private readonly IDomainService _domainService;

		public CreateEnvironmentCommandHandler(IMediator mediator, IDomainService domainService) : base(mediator)
		{
			_domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
		}

		public override async Task<Domain.Aggregates.EnvironmentAggregate.Environment> Handle(CreateEnvironmentCommand command, CancellationToken cancellationToken)
		{						
			return await _domainService.AddEnvironmentAsync(command.DesiredState);
		}
	}


	//TODO: Finalize Idempotency concept
	public class CreateEnvironmentIdentifiedCommandHandler : IdentifiedCommandHandler<CreateEnvironmentCommand, Domain.Aggregates.EnvironmentAggregate.Environment>
	{
		public CreateEnvironmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
		{
		}

		protected override Domain.Aggregates.EnvironmentAggregate.Environment CreateResultForDuplicateRequest()
		{
			return null;               
		}
	}
}
