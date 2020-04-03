using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeHeroes.OrderProcessing.Host.Worker.Application.Tasks
{
	public class SomeTask : IScheduledTask
	{
		public Guid Id { get; }

		public string Schedule => "0 5 * * *";

		public SomeTask(Guid? id = new Guid?())
		{
			if (!id.HasValue)
			{
				id = Guid.NewGuid();
			}

			Id = id.Value;
		}

		public async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			await Task.Delay(5000, cancellationToken);
		}
	}
}
