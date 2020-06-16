using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	//TODO: Review existing implementation (Ch3086)
	//TODO: Finalize value object(s) (Ch3086)
	public class DesiredState : ValueObject, IDesiredState
	{
		public string Name { get; protected set; }

		public string ApiVersion { get; protected set; }

		public IEnumerable<KeyValuePair<string, string>> Labels { get; protected set; }

		public Dictionary<string, string> Properties { get; protected set; }

		protected DesiredState() { }

		public DesiredState(string name, string apiVersion, IEnumerable<KeyValuePair<string, string>> labels = default, Dictionary<string, string> properties = default)
		{
			Name = name;
			ApiVersion = apiVersion;
			Labels = labels;
			Properties = properties;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
			yield return ApiVersion;
			yield return Labels;
			yield return Properties;
		}
	}
}
