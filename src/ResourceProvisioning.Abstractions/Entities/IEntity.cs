using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Events;

namespace ResourceProvisioning.Abstractions.Entities
{
	public interface IEntity<TKey> : IEntity where TKey : struct
	{
		TKey Id { get; }

		bool IsTransient();
	}

	public interface IEntity : IView, IValidatableObject
	{
		IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

		void AddDomainEvent(IDomainEvent @event);

		void RemoveDomainEvent(IDomainEvent @event);

		void ClearDomainEvents();
	}
}
