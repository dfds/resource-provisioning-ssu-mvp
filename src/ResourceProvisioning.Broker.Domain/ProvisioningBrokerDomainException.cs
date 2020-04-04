using System;

namespace ResourceProvisioning.Broker.Domain
{
	public sealed class ProvisioningBrokerDomainException : Exception
	{
		public ProvisioningBrokerDomainException()
		{ }

		public ProvisioningBrokerDomainException(string message)
			: base(message)
		{ }

		public ProvisioningBrokerDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
