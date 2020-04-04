using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class EnvironmentResourceReference : Entity<Guid>
	{
		public Guid ResourceId { get; private set; }

		public DateTime Provisioned { get; private set; }

		public string Comment { get; private set; }

		private EnvironmentResourceReference() : base()
		{
		}

		public EnvironmentResourceReference(Guid resourceId, DateTime provisioned, string comment) : this()
		{
			ResourceId = resourceId;
			Provisioned = provisioned;
			Comment = comment;
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Id == Guid.Empty)
			{ 
				yield return new ValidationResult($"Invalid resource id: {Id}");
			}
		}
	}
}
