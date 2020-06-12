namespace ResourceProvisioning.Cli.Core.Core.Authentication
{
	public class AuthenticationToken
	{
		public string IdToken { get; set; }
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public string Scope { get; set; }
		public int ExpiresIn { get; set; }
	}
}
