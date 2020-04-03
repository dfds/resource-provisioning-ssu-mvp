using System;
using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class EnvironmentStatus : BaseEnumeration
	{
		public static EnvironmentStatus Created = new EnvironmentStatus(1, nameof(Created).ToLowerInvariant());
		public static EnvironmentStatus Initializing = new EnvironmentStatus(2, nameof(Initializing).ToLowerInvariant());
		public static EnvironmentStatus Ready = new EnvironmentStatus(3, nameof(Ready).ToLowerInvariant());
		public static EnvironmentStatus Terminated = new EnvironmentStatus(4, nameof(Terminated).ToLowerInvariant());

		protected EnvironmentStatus()
		{
		}

		public EnvironmentStatus(int id, string name)
			: base(id, name)
		{
		}

		public static IEnumerable<EnvironmentStatus> List() =>
			new[] { Created, Initializing, Ready, Terminated };

		public static EnvironmentStatus FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new ArgumentException();
			}

			return state;
		}

		public static EnvironmentStatus From(int id)
		{
			var state = List().SingleOrDefault(s => s.Id == id);

			if (state == null)
			{
				throw new ArgumentException();
			}

			return state;
		}
	}
}
