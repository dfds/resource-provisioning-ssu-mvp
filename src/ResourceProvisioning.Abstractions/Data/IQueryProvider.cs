using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Data
{
	public interface IQueryProvider
	{
		Task<T> Query<T>(params object[] args) where T : class, IMaterializedView;
	}
}
