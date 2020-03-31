using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Events.Domain;

namespace ResourceProvisioning.Abstractions.Entities
{
	public abstract class BaseEntity<TKey> : IEntity<TKey> where TKey : struct
	{
		int? _requestedHashCode;

		private List<IDomainEvent> _domainEvents;

		public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

		[Required]
		public TKey Id { get; protected set; }

		protected BaseEntity()
		{

		}

		public void AddDomainEvent(IDomainEvent @event)
		{
			_domainEvents = _domainEvents ?? new List<IDomainEvent>();
			_domainEvents.Add(@event);
		}

		public void RemoveDomainEvent(IDomainEvent @event)
		{
			_domainEvents?.Remove(@event);
		}

		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}

		public bool IsTransient()
		{
			return this.Id.Equals(default(TKey));
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is BaseEntity<TKey>))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			if (this.GetType() != obj.GetType())
				return false;

			var item = (BaseEntity<TKey>)obj;

			if (item.IsTransient() || this.IsTransient())
				return false;
			else
				return item.Id.Equals(this.Id);
		}

		public override int GetHashCode()
		{
			if (!IsTransient())
			{
				if (!_requestedHashCode.HasValue)
					_requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

				return _requestedHashCode.Value;
			}
			else
				return base.GetHashCode();

		}
		public static bool operator ==(BaseEntity<TKey> left, BaseEntity<TKey> right)
		{
			if (Equals(left, null))
				return Equals(right, null) ? true : false;
			else
				return left.Equals(right);
		}

		public static bool operator !=(BaseEntity<TKey> left, BaseEntity<TKey> right)
		{
			return !(left == right);
		}


		public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
	}
}
