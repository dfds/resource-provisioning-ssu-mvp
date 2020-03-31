using System;

namespace ResourceProvisioning.Broker.Exceptions
{
	public class ContextDomainException : Exception
	{
		public ContextDomainException()
		{ }

		public ContextDomainException(string message)
			: base(message)
		{ }

		public ContextDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
