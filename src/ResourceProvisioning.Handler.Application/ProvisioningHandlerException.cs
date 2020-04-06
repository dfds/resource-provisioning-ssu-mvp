using System;

namespace ResourceProvisioning.Handler.Application
{
	public sealed class ProvisioningHandlerException : Exception
	{
		public ProvisioningHandlerException()
		{ }

		public ProvisioningHandlerException(string message)
			: base(message)
		{ }

		public ProvisioningHandlerException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
