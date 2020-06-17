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

		public ResourcePrincipal ResourcePrincipal { get; private set; }

		KeyValuePair<string, string> IActualState.ResourcePrincipal => new KeyValuePair<string, string>(ResourcePrincipal.PrincipalType, ResourcePrincipal.Value);

		public ActualState(string name, string apiVersion, IEnumerable<Label> labels = default, IEnumerable<Property> properties = default,
			DateTime created = default, DateTime lastUpdated = default, ResourcePrincipal resourcePrincipal = default) : base(name, apiVersion, labels, properties)
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
