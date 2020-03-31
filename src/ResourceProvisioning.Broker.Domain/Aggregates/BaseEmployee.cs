using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates
{
	public abstract class BaseEmployee : BaseEntity<Guid>, IEmployee
	{
		public string Name { get; protected set; }

		public string Email { get; protected set; }

		protected BaseEmployee() : base()
		{

		}

		public BaseEmployee(string name, string email) : this()
		{
			Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
			Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrEmpty(Name))
			{
				yield return new ValidationResult(nameof(Name));
			}

			if (string.IsNullOrEmpty(Email))
			{
				yield return new ValidationResult(nameof(Email));
			}
		}
	}
}
