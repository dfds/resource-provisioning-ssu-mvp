using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Behaviours
{
	public interface IStrategy<T>
	{
		ValueTask<T> Apply(T target);
	}
}
