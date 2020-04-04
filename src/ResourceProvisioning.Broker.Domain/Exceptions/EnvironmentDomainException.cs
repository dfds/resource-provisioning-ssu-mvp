﻿using System;

namespace ResourceProvisioning.Broker.Domain.Exceptions
{
	public sealed class EnvironmentDomainException : Exception
	{
		public EnvironmentDomainException()
		{ }

		public EnvironmentDomainException(string message)
			: base(message)
		{ }

		public EnvironmentDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
