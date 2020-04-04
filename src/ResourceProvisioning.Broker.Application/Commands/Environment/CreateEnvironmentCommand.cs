using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	[DataContract]
	public class CreateEnvironmentCommand : ICommand<EnvironmentRoot>
	{
		[DataMember]
		public DesiredState DesiredState { get; private set; }
				
		public CreateEnvironmentCommand(DesiredState desiredState)
		{
			DesiredState = desiredState;
		}
	}
}
