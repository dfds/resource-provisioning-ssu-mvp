using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Data
{
	public interface IMaterializedViewProvider
	{
		Task<IMaterializedView> MaterializeAsync(params object[] args);
	}
}
