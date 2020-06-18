using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	public sealed class Property : ValueObject
	{
		[Required]
		public string Key { get; private set; }

		public string Value { get; private set; }

		public Property(string key, string value)
		{
			Key = key;
			Value = value;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Key;
			yield return Value;
		}
	}
}
