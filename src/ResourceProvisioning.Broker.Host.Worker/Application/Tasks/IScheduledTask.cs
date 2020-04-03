using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeHeroes.OrderProcessing.Host.Worker.Application.Tasks
{
	public interface IScheduledTask
	{
		Guid Id { get; }

		string Schedule { get; }

		Task ExecuteAsync(CancellationToken cancellationToken);
	}
}
