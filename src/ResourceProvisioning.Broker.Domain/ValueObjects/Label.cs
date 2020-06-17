using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	public sealed class Label : ValueObject
	{
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
