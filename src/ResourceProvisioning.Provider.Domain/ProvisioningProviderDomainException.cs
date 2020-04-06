using System;

namespace ResourceProvisioning.Handler.Domain
{
	public sealed class ProvisioningProviderDomainException : Exception
	{
		public ProvisioningProviderDomainException()
		{ }

		public ProvisioningProviderDomainException(string message)
			: base(message)
		{ }

		public ProvisioningProviderDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
