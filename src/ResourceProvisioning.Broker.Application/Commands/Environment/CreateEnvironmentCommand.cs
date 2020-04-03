using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Application.Data;

namespace ResourceProvisioning.Broker.Application.Commands.Environment
{
	// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
	// In this case, its immutability is achieved by having all the setters as private
	// plus only being able to update the data just once, when creating the object through its constructor.
	// References on Immutable Commands:  
	// http://cqrs.nu/Faq
	// https://docs.spine3.org/motivation/immutability.html 
	// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
	// https://msdn.microsoft.com/en-us/library/bb383979.aspx

	[DataContract]
	public class CreateEnvironmentCommand : ICommand<bool>
	{
		[DataMember]
		public DesiredStateDto DesiredState { get; private set; }
				
		public CreateEnvironmentCommand(DesiredStateDto desiredState)
		{
			DesiredState = desiredState;
		}
	}
}
