using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ResourceProvisioning.Cli.Application.Authentication
{
	/// <summary>
	/// The <see cref="SecurityTokenExtensions"/> contains static methods related to <see cref="SecurityToken"/>.
	/// </summary>
	public static class SecurityTokenExtensions
    {
        /// <summary>
        /// Gets a Base64 encoded <see cref="string"/> representation of <paramref name="token"/>.
        /// </summary>
        /// <returns>
        /// Base64 encoded <see cref="string"/>.
        /// </returns>
        /// <example>
        /// <code>
        /// var token = new JwtSecurityToken();
        /// var encodedString = token.ToBase64String();
        /// </code>
        /// </example>
        /// <param name="token">The <see cref="SecurityToken"/> to encode.</param>
        public static string ToBase64String(this SecurityToken token)
        {
			var encodedTokenString = token switch
			{
				JwtSecurityToken jwt => jwt.RawData,
				_ => string.Empty
			};

            return encodedTokenString;
        }

        /// <summary>
        /// Detects the AuthenticationScheme <see cref="string"/> associated with <paramref name="token"/>.
        /// </summary>
        /// <returns>
        /// AuthenticationScheme as a <see cref="string"/>.
        /// </returns>
        /// <example>
        /// <code>
        /// var token = new JwtSecurityToken();
        /// var scheme = token.GetAuthenticationScheme();
        /// </code>
        /// </example>
        /// <param name="token">The <see cref="SecurityToken"/> instance that we infere the AuthenticationScheme string from.</param>
        public static string GetAuthenticationScheme(this SecurityToken token)
        {
			var authenticationSchema = token switch
			{
				JwtSecurityToken _ => "Bearer",
				_ => string.Empty
			};

			return authenticationSchema;
        }

        /// <summary>
        /// Converts <paramref name="token"/> to another <see cref="SecurityToken"/> using the specified <paramref name="handler"/> and optional <paramref name="descriptor"/>.
        /// </summary>
        /// <returns>
        /// Converted <see cref="SecurityToken"/>.
        /// </returns>
        /// <example>
        /// <code>
        /// var jwtToken = new JwtSecurityToken();
        /// var samlToken = jwtToken.Convert(new Saml2SecurityTokenHandler());
        /// </code>
        /// </example>
        /// <param name="token">The <see cref="SecurityToken"/> instance that we want to convert.</param>
        /// <param name="handler">The <see cref="SecurityTokenHandler"/> used for converting the token.</param>
        /// <param name="descriptor">The <see cref="SecurityTokenDescriptor"/> instance used to create the new token.</param>
        public static SecurityToken Convert(this SecurityToken token, SecurityTokenHandler handler, SecurityTokenDescriptor descriptor = null)
        {
            if (descriptor == null)
            {
                descriptor = new SecurityTokenDescriptor();

                if (token is JwtSecurityToken jwtToken)
                {
                    descriptor.Issuer = jwtToken.Issuer;
                    descriptor.Audience = jwtToken.Claims.Where(c => c.Type == "aud").Single().Value;
                    descriptor.Subject = new System.Security.Claims.ClaimsIdentity(jwtToken.Claims);
                    descriptor.SigningCredentials = jwtToken.SigningCredentials;
                    descriptor.EncryptingCredentials = jwtToken.EncryptingCredentials;
                    descriptor.IssuedAt = DateTime.UtcNow;
                }
            }

            return handler.CreateToken(descriptor);
        }
    }
}
