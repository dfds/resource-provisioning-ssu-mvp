using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate
{
	public class ContextResource : BaseEntity<Guid>
	{			   
		public Guid ResourceId { get; private set; }

		public string Comment { get; private set; }

		protected ContextResource() : base()
		{
		}

		public ContextResource(Guid resourceId, string comment) : this()
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
