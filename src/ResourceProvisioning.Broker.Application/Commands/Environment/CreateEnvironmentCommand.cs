using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	// TODO: Document that it is recommended to implement immutable Commands.  
	// http://cqrs.nu/Faq
	// https://docs.spine3.org/motivation/immutability.html 
	// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
	// https://msdn.microsoft.com/en-us/library/bb383979.aspx

	[DataContract]
	public class CreateEnvironmentCommand : ICommand<Domain.Aggregates.EnvironmentAggregate.Environment>
	{
		[DataMember]
		public DesiredState DesiredState { get; private set; }
				
		public CreateEnvironmentCommand(DesiredState desiredState)
		{
			DesiredState = desiredState;
		}
	}
}
