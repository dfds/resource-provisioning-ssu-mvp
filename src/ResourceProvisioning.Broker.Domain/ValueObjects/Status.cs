using System;
using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	//TODO: Review existing implementation (Ch3086)
	//TODO: Finalize value object(s) (Ch3086)
	public sealed class Status : ValueObject
	{
		public string Value { get; private set; }

		public bool IsAvailable { get; private set; }

		public string ReasonPhrase { get; private set; }

		public Uri ReasonUri { get; private set; }

		public Status(string value, bool isAvailable, string reasonPhrase = default, Uri reasonUri = default)
		{
			Value = value;
			IsAvailable = isAvailable;
			ReasonPhrase = reasonPhrase;
			ReasonUri = reasonUri;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Value;
			yield return IsAvailable;
			yield return ReasonPhrase;
			yield return ReasonUri;
		}
	}
}
