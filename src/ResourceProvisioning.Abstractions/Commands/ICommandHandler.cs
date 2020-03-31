using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
	{
	}
}
