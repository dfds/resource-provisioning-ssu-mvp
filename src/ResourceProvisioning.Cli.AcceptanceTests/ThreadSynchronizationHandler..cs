using System.Threading;

namespace ResourceProvisioning.Cli.AcceptanceTests
{
	public static class ThreadSynchronizationHandler
	{
		public static Mutex Mutex { get; set; } = new Mutex();
	}
}
