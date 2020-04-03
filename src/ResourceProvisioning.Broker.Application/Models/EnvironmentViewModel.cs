using System;
using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;

namespace ResourceProvisioning.Broker.Application.Models
{
	public class EnvironmentViewModel : IMaterializedView
	{
		public Guid OwnerId { get; set; }

		public DateTime CreateDate { get; set; }

		public string Description { get; set; }

		public string Status { get; set; }

		public DesiredState State { get; set; }

		public List<EnvironmentResourceViewModel> Resources { get; set; }
	}
}
