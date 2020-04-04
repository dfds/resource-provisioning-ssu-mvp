using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate
{
	public sealed class ResourceStatus : GridActorStatus
	{
		public new static ResourceStatus Initializing = new ResourceStatus(1, nameof(Initializing).ToLowerInvariant());
		public static ResourceStatus Ready = new ResourceStatus(2, nameof(Ready).ToLowerInvariant());
		public static ResourceStatus Unavailable = new ResourceStatus(4, nameof(Unavailable).ToLowerInvariant());

		public ResourceStatus(int id, string name) : base(id, name)
		{
		}

		public new static IEnumerable<ResourceStatus> List() => new[] { Initializing, Ready, Unavailable };
	}
}
