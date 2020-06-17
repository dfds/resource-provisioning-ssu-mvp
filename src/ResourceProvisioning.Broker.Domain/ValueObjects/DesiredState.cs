using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	public class DesiredState : ValueObject, IDesiredState
	{
		[Required]
		public string Name { get; protected set; }

		public string ApiVersion { get; protected set; }

		public IEnumerable<Label> Labels { get; protected set; }

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
