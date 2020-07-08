using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public interface IAuthenticationProvider
	{
		ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default);
	}
}
