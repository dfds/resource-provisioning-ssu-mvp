using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class Resource : BaseEntity<Guid>
	{			   
		public Guid ResourceId { get; private set; }

		public string Comment { get; private set; }

		protected Resource() : base()
		{
		}

		public Resource(Guid resourceId, string comment) : this()
		{
			ResourceId = resourceId;
			Comment = comment;
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ResourceId == Guid.Empty)
			{ 
				yield return new ValidationResult($"Invalid resource id: {ResourceId}");
			}
		}
	}
}
