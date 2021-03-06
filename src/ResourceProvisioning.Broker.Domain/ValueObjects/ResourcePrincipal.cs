﻿using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
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
