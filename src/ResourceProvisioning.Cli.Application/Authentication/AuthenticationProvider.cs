using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public abstract class AuthenticationProvider<TArgs> : IAuthenticationProvider<TArgs>
	{
		protected HttpClient HttpClient;
		protected CliApplicationOptions CliApplicationOptions;

		protected AuthenticationProvider(IOptions<CliApplicationOptions> cliApplicationOptions = default)
		{
			HttpClient = new HttpClient();
			CliApplicationOptions = cliApplicationOptions?.Value;
		}

		public abstract ValueTask<AuthenticationToken> Auth(TArgs args = default);
	}
}
