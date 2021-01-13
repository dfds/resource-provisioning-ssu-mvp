using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
	{
		protected readonly IMediator _mediator;

		protected CommandHandler(IMediator mediator = default)
		{
			_mediator = mediator;
		}

		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
	}
}
