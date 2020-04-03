using System;
using System.Runtime.Serialization;

namespace ResourceProvisioning.Broker.Application.Data
{
	public class EnvironmentResourceDto
	{
		[DataMember]
		public Guid ResourceId { get; set; }

		[DataMember]
		public string Comment { get; set; }
	}
}
