using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public abstract class BaseCommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
	{
		protected readonly IMediator _mediator;

		protected BaseCommandHandler(IMediator mediator) 
		{
			_mediator = mediator;
		}

		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
