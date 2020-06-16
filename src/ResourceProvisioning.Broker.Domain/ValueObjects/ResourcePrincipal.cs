using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	//TODO: Review existing implementation (Ch3086)
	//TODO: Finalize value object(s) (Ch3086)
	public sealed class ResourcePrincipal : ValueObject
	{
		public string PrincipalType { get; private set; }

		public string Value { get; private set; }

		public ResourcePrincipal(string principalType, string value)
		{
			PrincipalType = principalType;
			Value = value;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return PrincipalType;
			yield return Value;
		}
	}
}
