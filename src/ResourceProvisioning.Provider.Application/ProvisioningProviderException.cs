using System;

namespace ResourceProvisioning.Provider.Application
{
	public sealed class ProvisioningProviderException : Exception
	{
		public ProvisioningProviderException()
		{ }

		public ProvisioningProviderException(string message)
			: base(message)
		{ }

		public ProvisioningProviderException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
