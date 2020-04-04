using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Rules;

namespace ResourceProvisioning.Abstractions.Policies
{
	public interface IPolicy : IEventHandler<IEvent>
	{
		IRuleEvaluator Evaluator { get; }
	}
}
