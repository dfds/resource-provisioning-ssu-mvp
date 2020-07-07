using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	public abstract class AuthenticationCommand<T> : CliCommand<Task<int>> where T : class, IAuthentication, new()
	{		
		protected T AuthenticationProvider { get; }
		private CliApplicationOptions _cliApplicationOptions;

		protected AuthenticationCommand(IOptions<CliApplicationOptions> cliApplicationOptions) 
		{
			AuthenticationProvider = new T();
			_cliApplicationOptions = cliApplicationOptions.Value;
		}

		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			var response = await AuthenticationProvider.Auth(_cliApplicationOptions);

			Console.WriteLine(JsonSerializer.Serialize(response));

			return await Task.FromResult(0);
		}
	}
}
