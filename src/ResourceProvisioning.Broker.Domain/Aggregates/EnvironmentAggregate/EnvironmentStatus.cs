using System;
using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class EnvironmentStatus : BaseEnumeration
	{
		public static EnvironmentStatus Submitted = new EnvironmentStatus(1, nameof(Submitted).ToLowerInvariant());
		public static EnvironmentStatus Provisioning = new EnvironmentStatus(2, nameof(Provisioning).ToLowerInvariant());
		public static EnvironmentStatus Completed = new EnvironmentStatus(3, nameof(Completed).ToLowerInvariant());
		public static EnvironmentStatus Cancelled = new EnvironmentStatus(4, nameof(Cancelled).ToLowerInvariant());

		protected EnvironmentStatus()
		{
		}

		public EnvironmentStatus(int id, string name)
			: base(id, name)
		{
		}

		public static IEnumerable<EnvironmentStatus> List() =>
			new[] { Submitted, Provisioning, Completed, Cancelled };

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
