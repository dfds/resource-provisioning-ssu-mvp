using System.Threading;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Behaviours
{
	public interface IStrategy<T>
	{
		ValueTask<T> Apply(T target, CancellationToken cancellationToken = default);
	}
}
