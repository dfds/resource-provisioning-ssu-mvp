﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ResourceProvisioning.Abstractions.Commands
{
	public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
	{
		new Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
	}
}
