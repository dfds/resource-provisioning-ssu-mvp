using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Broker.Host.Api.Application.Data;

namespace ResourceProvisioning.Broker.Host.Api.Application.Commands.Environment
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
		private readonly List<EnvironmentResourceDto> _resources;

		[DataMember]
		public Guid EmployeeId { get; private set; }

		[DataMember]
		public string EmployeeName { get; private set; }

		[DataMember]
		public string EmployeeEmail { get; private set; }

		[DataMember]
		public IEnumerable<EnvironmentResourceDto> Resources => _resources.AsReadOnly();
				
		public CreateEnvironmentCommand(Guid employeeId, string employeeName, string employeeEmail, List<EnvironmentResourceDto> resources)
		{
			EmployeeId = employeeId;
			EmployeeName = employeeName;
			EmployeeEmail = employeeEmail;
			_resources = resources;
		}
	}
}
