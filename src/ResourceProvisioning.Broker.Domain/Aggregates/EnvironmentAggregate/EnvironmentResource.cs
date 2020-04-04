using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class EnvironmentResource : BaseEntity<Guid>
	{			   
		public DateTime Provisioned { get; private set; }

		public bool IsDesired { get; private set; }

		public string Comment { get; private set; }

		private EnvironmentResource() : base()
		{
		}

		public EnvironmentResource(Guid id, DateTime provisioned, string comment, bool isDesired) : this()
		{
			Id = id;
			Provisioned = provisioned;
			Comment = comment;
			IsDesired = isDesired;
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
