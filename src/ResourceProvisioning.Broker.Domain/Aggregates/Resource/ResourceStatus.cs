using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Domain.Aggregates.Resource
{
	public sealed class ResourceStatus : GridActorStatus
	{
		public static ResourceStatus Ready = new ResourceStatus(8, nameof(Ready).ToLowerInvariant());
		public static ResourceStatus Unavailable = new ResourceStatus(16, nameof(Unavailable).ToLowerInvariant());

		public ResourceStatus(int id, string name) : base(id, name)
		{
		}

		public static new IEnumerable<ResourceStatus> List()
		{
			var result = GridActorStatus.List().Cast<ResourceStatus>().ToList();

			result.Add(Ready);
			result.Add(Unavailable);

			return result.AsEnumerable();
		}
	}
}
