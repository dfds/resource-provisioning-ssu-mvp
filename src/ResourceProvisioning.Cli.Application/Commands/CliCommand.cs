using System;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ResourceProvisioning.Cli.Application.Commands
{
	/// <summary>
	/// This base type provides shared functionality.
	/// Also, declaring <see cref="HelpOptionAttribute"/> on this type means all types that inherit from it
	/// will automatically support '--help'
	/// </summary>
	[HelpOption("--help")]
	public abstract class CliCommand
	{
		private string _environmentId = Guid.Empty.ToString();

		[Option("-e|--environmentId", CommandOptionType.SingleOrNoValue)]
		public string EnvironmentId
		{
			get
			{
				return _environmentId;
			}
			set
			{
				if (!Guid.TryParse(value, out _))
				{
					throw new ArgumentException("EnvironmentId is not a valid GUID.");
				}

				_environmentId = value;
			}
		}

		public abstract Task<int> OnExecuteAsync(CancellationToken cancellationToken = default);
	}
}
