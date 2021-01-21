using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Behaviours
{
	public interface IStrategy<T>
	{
		Task Apply(T target);
	}
}
