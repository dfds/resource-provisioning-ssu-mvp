using System;
using System.Collections.Generic;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IActualState : IDesiredState
	{
		DateTime Created { get; }

		DateTime LastUpdated { get; }

		KeyValuePair<string, string> ResourcePrincipal { get;}
	}
}
