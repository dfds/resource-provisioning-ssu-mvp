using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Cli.Application.Authentication;

namespace ResourceProvisioning.Cli.Application.Commands.Login
{
	public abstract class AuthenticationCommand<T> : CliCommand<Task<int>> where T : class, IAuthenticationFlow
	{		
		protected T AuthenticationProvider { get; }

		protected AuthenticationCommand(IOptions<CliApplicationOptions> cliApplicationOptions)
		{
			var providerType = typeof(T);
			var searchFilter = new[] {typeof(IOptions<CliApplicationOptions>)};

			if (providerType.GetConstructor(searchFilter) == null)
			{
				throw new ArgumentException(nameof(T));
;			}

			AuthenticationProvider = Activator.CreateInstance(typeof(T), args: new object[] { cliApplicationOptions }) as T;
		}

		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			var response = await AuthenticationProvider.Auth();

			Console.WriteLine(JsonSerializer.Serialize(response));

			return await Task.FromResult(0);
		}
	}
}
