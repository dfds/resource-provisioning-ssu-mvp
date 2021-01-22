using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Aggregates
{
	public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot where TKey : struct
	{

	}
}
