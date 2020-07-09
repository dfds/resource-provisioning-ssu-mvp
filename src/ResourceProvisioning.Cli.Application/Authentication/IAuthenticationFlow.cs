using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public interface IAuthenticationFlow
	{
		ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default);
	}
}
