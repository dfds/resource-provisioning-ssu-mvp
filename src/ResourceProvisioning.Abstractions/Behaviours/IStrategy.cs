using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Behaviours
{
	public interface IStrategy
	{
		ValueTask<T> Apply<T>(T target);
	}
}
