using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	[DataContract]
	public sealed class GetEnvironmentCommand : ICommand<IEnumerable<EnvironmentRoot>>, IProvisioningRequest
	{
		[DataMember]
		public Guid EnvironmentId { get; private set; }

		public GetEnvironmentCommand(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
