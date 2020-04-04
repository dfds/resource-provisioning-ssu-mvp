using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	[DataContract]
	public class CreateEnvironmentCommand : ICommand<EnvironmentRoot>, IProvisioningRequest
	{
		[DataMember]
		public DesiredState DesiredState { get; private set; }
				
		public CreateEnvironmentCommand(DesiredState desiredState)
		{
			DesiredState = desiredState;
		}
	}
}
