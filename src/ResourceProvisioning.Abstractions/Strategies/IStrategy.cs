using System.Threading;

namespace ResourceProvisioning.Abstractions.Strategies
{
	public interface IStrategy<T>
	{
		void Apply(T target, CancellationToken cancellationToken = default);
	}
}
