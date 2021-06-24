namespace ResourceProvisioning.Cli.Application.Authentication.Flows
{
	public partial class UsernamePasswordFlow : AuthenticationFlow
	{
		class UserData
		{
			public string Username { get; set; }
			public string Password { get; set; }
		}
	}
}
