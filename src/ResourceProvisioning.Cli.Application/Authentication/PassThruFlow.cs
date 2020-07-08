using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	public class PassThruFlow : AuthenticationProvider<AuthenticationToken>
	{
		public PassThruFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override ValueTask<AuthenticationToken> Auth(AuthenticationToken args = default)
		{
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", args?.AccessToken);

            return new ValueTask<AuthenticationToken>(args);
		}
	}
}
