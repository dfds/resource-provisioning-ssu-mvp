namespace ResourceProvisioning.Cli.Application.Configuration
{
	public sealed class AuthenticationSection
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public string HostName { get; set; } = "localhost";

		public string Instance { get; set; } = "https://login.microsoftonline.com";

		public string ClientId { get; set; } = "72d0443b-ff34-4568-8eb9-1d81849c5462";

		public string TenantId { get; set; } = "73a99466-ad05-4221-9f90-e7142aa2f6c1";
	}
}
