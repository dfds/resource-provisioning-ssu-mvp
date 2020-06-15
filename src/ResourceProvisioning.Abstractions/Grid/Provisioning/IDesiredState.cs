using System.Collections.Generic;

namespace ResourceProvisioning.Abstractions.Grid.Provisioning
{
	public interface IDesiredState
	{
		string Name { get; }

		string ApiVersion { get; }

		IEnumerable<KeyValuePair<string, string>> Labels { get; }

		Dictionary<string, string> Properties { get; }
	}
}
