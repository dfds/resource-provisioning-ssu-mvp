using System;

namespace ResourceProvisioning.Abstractions.Infrastructure
{
	[Flags]
	public enum GridActorType
	{
		None = 0,
		User = 1,
		System = 2,
		External = 4
	}
}
