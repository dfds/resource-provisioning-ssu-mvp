using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
	{
		//TODO: Make sure that overriding the IRequestHandler signature does not mess up the mediatr internals (its shouldnt matter, famous last words). (Ch2139)
		new Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
	}
}
