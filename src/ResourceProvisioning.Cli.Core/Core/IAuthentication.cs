using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Authentication;

namespace ResourceProvisioning.Cli.Core.Core
{
	public interface IAuthentication
	{
		public Task<AuthenticationToken> Auth();
	}
}
