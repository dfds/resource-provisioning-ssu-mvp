using System;

namespace ResourceProvisioning.Cli.Domain
{
	public sealed class CliDomainException : Exception
	{
		public CliDomainException()
		{ }

		public CliDomainException(string message)
			: base(message)
		{ }

		public CliDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
