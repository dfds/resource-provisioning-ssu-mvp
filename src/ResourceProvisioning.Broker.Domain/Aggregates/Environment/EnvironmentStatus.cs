using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Domain.Aggregates.Environment
{
	public sealed class EnvironmentStatus : GridActorStatus
	{
		public static EnvironmentStatus Requested = new EnvironmentStatus(8, nameof(Requested).ToLowerInvariant());

		public EnvironmentStatus(int id, string name) : base(id, name)
		{
		}

		public static new IEnumerable<EnvironmentStatus> List()
		{
			var result = GridActorStatus.List().Cast<EnvironmentStatus>().ToList();

			result.Add(Requested);

			return result.AsEnumerable();
		}
	}
}
