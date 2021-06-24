using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public abstract class AuthenticationFlow : IAuthenticationFlow
	{
		protected HttpClient HttpClient;
		protected CliApplicationOptions CliApplicationOptions;

		protected AuthenticationFlow(IOptions<CliApplicationOptions> cliApplicationOptions = default)
		{
			HttpClient = new HttpClient();
			CliApplicationOptions = cliApplicationOptions?.Value;
		}

		public abstract ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default);
	}
}
