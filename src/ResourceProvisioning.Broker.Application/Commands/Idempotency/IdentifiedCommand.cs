using System;
using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Broker.Application.Commands.Idempotency
{
	public sealed class IdentifiedCommand<T, R> : ICommand<R> where T : ICommand<R>
	{
		public T Command { get; }

		public Guid Id { get; }

		public IdentifiedCommand(T command, Guid id)
		{
			Command = command;
			Id = id;
		}
	}
}
