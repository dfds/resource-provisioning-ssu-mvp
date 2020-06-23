using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public interface IAuthentication
	{
		Task<AuthenticationToken> Auth();
	}
}
