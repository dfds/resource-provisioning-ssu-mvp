using System;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IActualState : IDesiredState
	{
		DateTime Created { get; }

		DateTime LastUpdated { get; }
	}
}
