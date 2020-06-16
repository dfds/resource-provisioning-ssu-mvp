using System.Linq;
using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	//TODO: Review existing implementation (Ch3086)
	//TODO: Finalize value object(s) (Ch3086)
	public class DesiredState : ValueObject, IDesiredState
	{
		public string Name { get; protected set; }

		public string ApiVersion { get; protected set; }

		[NotMapped]
		public IEnumerable<Label> Labels { get; protected set; }

		[NotMapped]
		public IEnumerable<Property> Properties { get; protected set; }

		IEnumerable<KeyValuePair<string, string>> IDesiredState.Properties => Properties.Select(p => new KeyValuePair<string, string>(p.Key, p.Value));

		IEnumerable<KeyValuePair<string, string>> IDesiredState.Labels => Labels.Select(l => new KeyValuePair<string, string>(l.Name, l.Value));

		protected DesiredState() { }

		public DesiredState(string name, string apiVersion, IEnumerable<Label> labels = default, IEnumerable<Property> properties = default)
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
