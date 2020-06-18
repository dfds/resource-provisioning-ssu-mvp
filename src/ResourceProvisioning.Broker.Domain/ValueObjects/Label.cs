using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	public sealed class Label : ValueObject
	{
		[Required]
		public string Name { get; private set; }

		public string Value { get; private set; }

		public Label(string name, string value)
		{
			Name = name;
			Value = value;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
			yield return Value;
		}
	}
}
