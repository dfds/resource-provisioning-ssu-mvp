using System.Threading.Tasks;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public interface IAuthenticationProvider<in TArgs>
	{
		ValueTask<AuthenticationToken> Auth(TArgs args = default);
	}
}
