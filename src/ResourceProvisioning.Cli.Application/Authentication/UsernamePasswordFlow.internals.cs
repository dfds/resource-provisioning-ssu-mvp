namespace ResourceProvisioning.Cli.Application.Authentication
{
	public partial class UsernamePasswordFlow : AuthenticationProvider
	{
		class UserData
		{
			public string Username { get; set; }
			public string Password { get; set; }
		}
	}
}
