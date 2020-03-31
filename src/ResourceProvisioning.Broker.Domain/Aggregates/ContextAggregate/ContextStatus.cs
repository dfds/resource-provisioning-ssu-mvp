using System;
using System.Collections.Generic;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate
{
	public class ContextStatus : BaseEnumeration
	{
		public static ContextStatus Submitted = new ContextStatus(1, nameof(Submitted).ToLowerInvariant());
		public static ContextStatus AwaitingApproval = new ContextStatus(2, nameof(AwaitingApproval).ToLowerInvariant());
		public static ContextStatus Approved = new ContextStatus(3, nameof(Approved).ToLowerInvariant());
		public static ContextStatus InProgress = new ContextStatus(4, nameof(InProgress).ToLowerInvariant());
		public static ContextStatus Completed = new ContextStatus(5, nameof(Completed).ToLowerInvariant());
		public static ContextStatus Cancelled = new ContextStatus(6, nameof(Cancelled).ToLowerInvariant());

		protected ContextStatus()
		{
		}

		public ContextStatus(int id, string name)
			: base(id, name)
		{
		}

		public static IEnumerable<ContextStatus> List() =>
			new[] { Submitted, AwaitingApproval, Approved, InProgress, Completed, Cancelled };

		public static ContextStatus FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new ArgumentException();
			}

			return state;
		}

		public static ContextStatus From(int id)
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
