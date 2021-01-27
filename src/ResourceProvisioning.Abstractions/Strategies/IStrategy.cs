using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Strategies
{
	public interface IStrategy<T>
	{
		Task Apply(T target, CancellationToken cancellationToken = default);
	}
}
