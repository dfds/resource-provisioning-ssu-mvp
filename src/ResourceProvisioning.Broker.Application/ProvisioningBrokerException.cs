using System;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ProvisioningBrokerException : Exception
	{
		public ProvisioningBrokerException()
		{ }

		public ProvisioningBrokerException(string message)
			: base(message)
		{ }

		public ProvisioningBrokerException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
