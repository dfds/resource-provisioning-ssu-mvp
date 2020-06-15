using System;
using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Domain.ValueObjects
{
	public sealed class ActualState : DesiredState, IActualState
	{
		public DateTime Created { get; private set; }

		public DateTime LastUpdated { get; private set; }

		public Status Status { get; private set; }

		public KeyValuePair<string, string> ResourcePrincipal { get; private set; }

		public ActualState(string name, string apiVersion, IEnumerable<KeyValuePair<string, string>> labels = default, Dictionary<string, string> properties = default,
			DateTime created = default, DateTime lastUpdated = default, KeyValuePair<string, string> resourcePrincipal = default) : base(name, apiVersion, labels, properties)
		{
			Created = created;
			LastUpdated = lastUpdated;
			ResourcePrincipal = resourcePrincipal;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			base.GetAtomicValues();

			yield return Created;
			yield return LastUpdated;
			yield return ResourcePrincipal;
		}
	}
}
