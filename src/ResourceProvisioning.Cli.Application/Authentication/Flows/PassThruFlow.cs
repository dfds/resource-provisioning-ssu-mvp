using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	public class PassThruFlow : AuthenticationFlow
	{
		public PassThruFlow(IOptions<CliApplicationOptions> cliApplicationOptions) : base(cliApplicationOptions)
		{
		}

		public override ValueTask<SecurityToken> Auth(SecurityTokenDescriptor descriptor = default)
		{
			var jwt = new JwtSecurityToken().Convert(new JwtSecurityTokenHandler(), descriptor);

			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwt.GetAuthenticationScheme(), jwt.ToBase64String());

            return new ValueTask<SecurityToken>(jwt);
		}
	}
}
