using System;
using ResourceProvisioning.Broker.Exceptions;

namespace ResourceProvisioning.Broker.Infrastructure.Exceptions
{
	public class ContextProcessingInfrastructureException : ContextDomainException
	{
		public ContextProcessingInfrastructureException()
		{ }

		public ContextProcessingInfrastructureException(string message)
			: base(message)
		{ }

		public ContextProcessingInfrastructureException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
