using System;
using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	[DataContract]
	public sealed class DeleteEnvironmentCommand : ICommand<bool>, IProvisioningRequest
	{
		[DataMember]
		public Guid EnvironmentId { get; private set; }

		public DeleteEnvironmentCommand(Guid environmentId)
		{
			EnvironmentId = environmentId;
		}
	}
}
