using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Rules
{
	interface IRuleEvaluator
	{
		IEnumerable<IRule> Rules { get; }

		Task Evaluate<T>(T entity) where T : IEntity;
	}
}
