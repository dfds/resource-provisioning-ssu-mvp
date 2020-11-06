using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Commands;

namespace ResourceProvisioning.Abstractions.Facade
{
	public interface IFacade
	{
		Task<T> Execute<T>(ICommand<T> command, CancellationToken cancellationToken = default);
	}
}
