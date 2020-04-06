using System;

namespace ResourceProvisioning.Handler.Domain
{
	public sealed class ProvisioningHandlerDomainException : Exception
	{
		public ProvisioningHandlerDomainException()
		{ }

		public ProvisioningHandlerDomainException(string message)
			: base(message)
		{ }

		public ProvisioningHandlerDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
