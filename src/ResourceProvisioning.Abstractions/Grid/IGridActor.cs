using System;

namespace ResourceProvisioning.Abstractions.Grid
{
	public interface IGridActor
	{
		Guid Id { get; }

		GridActorType Type { get; }
	}
}
