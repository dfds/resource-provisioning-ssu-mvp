using System;

namespace ResourceProvisioning.Abstractions.Infrastructure
{
	interface IGridActor
	{
		Guid Id { get; }

		GridActorType Type { get; }
	}
}
