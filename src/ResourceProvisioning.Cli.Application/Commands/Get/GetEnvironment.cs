using System;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Domain.Services;

namespace ResourceProvisioning.Cli.Application.Commands.Get
{
	[Command(Name="environment")]
	public sealed class GetEnvironment : CliCommand<Task<int>>
	{
		private readonly IBrokerService _broker;

		public GetEnvironment(IBrokerService broker)
		{
			_broker = broker;
		}

		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			var actualState = _broker.GetCurrentStateByEnvironmentAsync(Guid.Parse(EnvironmentId), cancellationToken);

			actualState.Wait();

			Console.WriteLine(actualState.Result);

			return await Task.FromResult(0);
		}
	}
}
