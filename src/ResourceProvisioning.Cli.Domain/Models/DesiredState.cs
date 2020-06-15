using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Cli.Application.Models
{
	public class DesiredState : IDesiredState
	{
		public string Name { get; set; }

		public string ApiVersion { get; set; }

		public IEnumerable<KeyValuePair<string, string>> Labels { get; set; }

		public Dictionary<string, string> Properties { get; set; }
	}
}
