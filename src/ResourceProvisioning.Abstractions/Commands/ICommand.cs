using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public interface ICommand<out TResponse> : IRequest<TResponse>
	{
	}
}
