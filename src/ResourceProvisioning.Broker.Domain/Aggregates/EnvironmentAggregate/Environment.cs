using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Exceptions;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class Environment : BaseEntity<Guid>, IAggregateRoot
	{
		// DDD Patterns comment:
		// Using a private collection field, better for DDD Aggregate's encapsulation
		// so resources cannot be added from "outside the AggregateRoot" directly to the collection,
		// but only through the method Environment.AddResource() which includes behaviour.
		private List<EnvironmentResource> _resources;

		public DesiredState State { get; private set; }

		// EF comment: When using EF its typically good practice to bundle the FKEY fields with their corresponding navigation properties.
		public EnvironmentStatus Status { get; private set; }
		private int _statusId = EnvironmentStatus.Created.Id;

		public DateTime CreateDate => DateTime.UtcNow;

		public string Description { get; private set; }

		public Guid OwnerId { get; private set; }

		public IEnumerable<EnvironmentResource> Resources => _resources.AsReadOnly();

		protected Environment() : base()
		{
			_resources = new List<EnvironmentResource>();
		}

		public Environment(DesiredState desiredState) : this()
		{
			State = desiredState;
			
			AddDomainEvent(new EnvironmentCreatedEvent(this));
		}

		// DDD Patterns comment
		// This Environment AggregateRoot's method "AddResource()" should be the only way to add a Resource to the Environment,
		// so any behavior and validations are controlled by the AggregateRoot in order to maintain consistency between the whole Aggregate. 
		public void AddResource(Guid resourceId, string comment)
		{
			//TODO: Adding a resource should sync with the desired state.
			var existingResource = _resources.Where(o => o.ResourceId == resourceId).SingleOrDefault();

			if (existingResource == null)
			{        
				var resource = new EnvironmentResource(resourceId, comment);

				var validationResult = resource.Validate(new ValidationContext(resource));

				if (validationResult.Any())
				{
					var innerException = new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

					throw new EnvironmentDomainException(nameof(resource), innerException);
				}

				_resources.Add(resource);
			}
		}

		public void SetInitializingStatus()
		{
			if (_statusId == EnvironmentStatus.Created.Id)
			{
				AddDomainEvent(new EnvironmentStatusChangedToInitializingEvent(Id));

				_statusId = EnvironmentStatus.Initializing.Id;
				Description = "Initializing";
			}
		}

		public void SetReadyStatus()
		{
			if (_statusId == EnvironmentStatus.Initializing.Id)
			{
				AddDomainEvent(new EnvironmentStatusChangedToReadyEvent(Id));

				_statusId = EnvironmentStatus.Ready.Id;
				Description = "Ready";
			}
		}

		public void SetTerminatedStatus()
		{
			if (_statusId == EnvironmentStatus.Terminated.Id)
			{
				throw new EnvironmentDomainException($"{Status.Name} -> {EnvironmentStatus.Terminated.Name}");
			}

			_statusId = EnvironmentStatus.Terminated.Id;
			Description = "Terminated";

			AddDomainEvent(new EnvironmentStatusChangedToTerminatedEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (State == null)
			{
				yield return new ValidationResult(nameof(State));
			}
		}
	}
}
