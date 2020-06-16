using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	//TODO: Review existing implementation (Ch3086)
	//TODO: Finalize value object(s) (Ch3086)
	public sealed class Property : ValueObject
	{
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
