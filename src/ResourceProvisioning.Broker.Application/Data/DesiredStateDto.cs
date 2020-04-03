using System;
using System.Runtime.Serialization;

namespace ResourceProvisioning.Broker.Application.Data
{
	public class DesiredStateDto
	{
		[DataMember]
		public string Option1 { get; private set; }

		[DataMember]
		public string Option2 { get; private set; }

		[DataMember]
		public string Option3 { get; private set; }

		[DataMember]
		public string Option4 { get; private set; }
	}
}
