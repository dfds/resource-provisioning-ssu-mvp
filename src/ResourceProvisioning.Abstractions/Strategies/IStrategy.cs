using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Strategies
{
	public interface IStrategy<T>
	{
		ValueTask<T> Apply(T target, CancellationToken cancellationToken = default);
	}
}
