using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class DesiredState : BaseValueObject
	{
		public string Option1 { get; private set; }

		public string Option2 { get; private set; }

		public string Option3 { get; private set; }

		public string Option4 { get; private set; }

		private DesiredState() { }

		public DesiredState(string option1, string option2, string option3, string option4)
		{
			Option1 = option1;
			Option2 = option2;
			Option3 = option3;
			Option4 = option4;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			// Using a yield return statement to return each element one at a time
			yield return Option1;
			yield return Option2;
			yield return Option3;
			yield return Option4;
		}
	}
}
