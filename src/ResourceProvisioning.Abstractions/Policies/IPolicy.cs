using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Policies
{
	interface IPolicy<T>
	{
		Task Apply(T target);
    }
}
