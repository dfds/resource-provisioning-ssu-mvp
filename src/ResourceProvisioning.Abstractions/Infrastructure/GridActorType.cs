using System;

namespace ResourceProvisioning.Abstractions.Infrastructure
{
	[Flags]
	public enum GridActorType 
	{
		User = 1,
		System = 2,
		External = 4
	}
}
