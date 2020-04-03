using System;
using ResourceProvisioning.Abstractions.Data;

namespace ResourceProvisioning.Broker.Application.Models
{
	public class EnvironmentResourceViewModel : IMaterializedView
	{
		public Guid ResourceId { get; set; }

		public string Comment { get; set; }
	}
}
