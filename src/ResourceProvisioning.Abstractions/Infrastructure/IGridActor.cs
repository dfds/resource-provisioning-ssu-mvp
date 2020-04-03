using System;

namespace ResourceProvisioning.Abstractions.Infrastructure
{
	public interface IGridActor
	{
		Guid Id { get; }

		GridActorType Type { get; }
	}
}
