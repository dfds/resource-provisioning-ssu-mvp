using System;

namespace ResourceProvisioning.Broker.Infrastructure.Exceptions
{
	public class BrokerException : Exception
	{
		public BrokerException()
		{ }

		public BrokerException(string message)
			: base(message)
		{ }

		public BrokerException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
