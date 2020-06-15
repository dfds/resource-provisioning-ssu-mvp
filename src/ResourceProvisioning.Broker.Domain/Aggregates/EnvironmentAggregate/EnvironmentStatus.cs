using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class EnvironmentStatus : GridActorStatus
	{
		public static EnvironmentStatus Created = new EnvironmentStatus(8, nameof(Created).ToLowerInvariant());
		public static EnvironmentStatus Requested = new EnvironmentStatus(16, nameof(Requested).ToLowerInvariant());

		public EnvironmentStatus(int id, string name) : base(id, name)
		{
		}

		public static new IEnumerable<EnvironmentStatus> List() 
		{
			var result = GridActorStatus.List().Cast<EnvironmentStatus>().ToList();

			result.Add(Created);

			return result.AsEnumerable();
		}
	}
}
