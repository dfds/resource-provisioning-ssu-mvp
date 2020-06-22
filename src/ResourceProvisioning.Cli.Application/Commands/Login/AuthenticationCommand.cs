using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	public abstract class AuthenticationCommand<T> : CliCommand<Task<int>> where T : class, IAuthentication, new()
	{		
		protected T AuthenticationProvider { get; }

		protected AuthenticationCommand() 
		{
			AuthenticationProvider = new T();
		}

		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			var response = await AuthenticationProvider.Auth();

			Console.WriteLine(JsonSerializer.Serialize(response));

			return await Task.FromResult(0);
		}
	}
}
