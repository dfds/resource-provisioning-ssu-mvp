using System;
using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Grid
{
	public class GridActorStatus : EntityEnumeration
	{
		public static GridActorStatus Initializing = new GridActorStatus(1, nameof(Initializing).ToLowerInvariant());
		public static GridActorStatus Started = new GridActorStatus(2, nameof(Started).ToLowerInvariant());
		public static GridActorStatus Stopped = new GridActorStatus(4, nameof(Stopped).ToLowerInvariant());

		protected GridActorStatus()
		{
		}

		public GridActorStatus(int id, string name)
			: base(id, name)
		{
		}

		public static IEnumerable<GridActorStatus> List() => new[] { Initializing, Started, Stopped };

		public static GridActorStatus FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new ArgumentException();
			}

			return state;
		}

		public static GridActorStatus From(int id)
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
