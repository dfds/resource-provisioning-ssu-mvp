using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Events
{
	public interface IEventStore
	{
		Task SaveAsync<TKey>(TKey indexer, IEnumerable<IEvent> events) where TKey : struct;

		Task<IEnumerable<IEvent>> GetAsync<TKey>(TKey indexer) where TKey : struct;
	}
}
