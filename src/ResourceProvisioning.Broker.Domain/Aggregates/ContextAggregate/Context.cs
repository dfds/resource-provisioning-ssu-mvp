using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Broker.Events.Domain;
using ResourceProvisioning.Broker.Exceptions;

namespace ResourceProvisioning.Broker.Domain.Aggregates.ContextAggregate
{
	public class Context : BaseEntity<Guid>, IAggregateRoot
	{
		// DDD Patterns comment:
		// Using a private collection field, better for DDD Aggregate's encapsulation
		// so ContextResources cannot be added from "outside the AggregateRoot" directly to the collection,
		// but only through the method ContextAggrergateRoot.AddResource() which includes behaviour.
		private List<Resource> _contextResources;

		public DesiredState TargetState { get; private set; }
		
		// EF comment: When using EF its typically good practice to bundle the FKEY fields with their corresponding navigation properties.
		public ContextStatus ContextStatus { get; private set; }
		private int _contextStatusId;

		public DateTime CreateDate { get; private set; }

		public string Description { get; private set; }

		public Guid OwnerId { get; private set; }

		public IEnumerable<Resource> ContextResources => _contextResources.AsReadOnly();

		protected Context() : base()
		{
			_contextResources = new List<Resource>();
		}

		public Context(Guid employeeId, string employeeName, string employeeEmail, Guid? ownerId = null) : this()
		{
			_contextStatusId = ContextStatus.Submitted.Id;
			CreateDate = DateTime.UtcNow;
			OwnerId = ownerId ?? employeeId;
			TargetState = new DesiredState(employeeId.ToString(), employeeName, employeeEmail, string.Empty);
			
			AddDomainEvent(new ContextCreatedDomainEvent(this));
		}

		// DDD Patterns comment
		// This Context AggregateRoot's method "AddResource()" should be the only way to add a Resource to the Context,
		// so any behavior and validations are controlled by the AggregateRoot in order to maintain consistency between the whole Aggregate. 
		public void AddResource(Guid resourceId, string comment)
		{
			var existingResource = _contextResources.Where(o => o.ResourceId == resourceId).SingleOrDefault();

			if (existingResource == null)
			{        
				var resource = new Resource(resourceId, comment);

				var validationResult = resource.Validate(new ValidationContext(resource));

				if (validationResult.Any())
				{
					var innerException = new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

					throw new ContextDomainException(nameof(resource), innerException);
				}

				_contextResources.Add(resource);
			}
		}

		public void SetAwaitingApprovalStatus()
		{
			if (_contextStatusId == ContextStatus.Submitted.Id)
			{
				AddDomainEvent(new ContextStatusChangedToAwaitingApprovalDomainEvent(Id));

				_contextStatusId = ContextStatus.AwaitingApproval.Id;
			}
		}

		public void SetApprovedStatus()
		{
			if (_contextStatusId == ContextStatus.AwaitingApproval.Id)
			{
				AddDomainEvent(new ContextStatusChangedToApprovedDomainEvent(Id));

				_contextStatusId = ContextStatus.Approved.Id;
				Description = "Approved";
			}
		}

		public void SetInProgressStatus()
		{
			if (_contextStatusId == ContextStatus.Approved.Id)
			{
				AddDomainEvent(new ContextStatusChangedToInProgressDomainEvent(Id));

				_contextStatusId = ContextStatus.InProgress.Id;
				Description = "InProgress";
			}
		}

		public void SetCompletedStatus()
		{
			if (_contextStatusId == ContextStatus.InProgress.Id)
			{
				AddDomainEvent(new ContextStatusChangedToCompletedDomainEvent(Id));

				_contextStatusId = ContextStatus.Completed.Id;
				Description = "Completed";
			}
		}

		public void SetCancelledStatus()
		{
			if (_contextStatusId == ContextStatus.Completed.Id)
			{
				throw new ContextDomainException($"{ContextStatus.Name} -> {ContextStatus.Cancelled.Name}");
			}

			_contextStatusId = ContextStatus.Cancelled.Id;
			Description = "Cancelled";

			AddDomainEvent(new ContextStatusChangedToCancelledDomainEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TargetState == null)
			{
				yield return new ValidationResult(nameof(TargetState));
			}
		}
	}
}
