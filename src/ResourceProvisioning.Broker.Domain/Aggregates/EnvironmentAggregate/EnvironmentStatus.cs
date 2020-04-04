using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Grid;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class EnvironmentStatus : GridActorStatus
	{
		public static EnvironmentStatus Created = new EnvironmentStatus(8, nameof(Created).ToLowerInvariant());
		
		public EnvironmentStatus(int id, string name) : base(id, name)
		{
		}

		public new static IEnumerable<EnvironmentStatus> List() 
		{
			var result = GridActorStatus.List().Cast<EnvironmentStatus>().ToList();

			result.Add(Created);

			return result.AsEnumerable();
		}
	}
}
