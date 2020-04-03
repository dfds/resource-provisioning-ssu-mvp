﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Broker.Domain.Events.Domain;
using ResourceProvisioning.Broker.Exceptions;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public class Environment : BaseEntity<Guid>, IAggregateRoot
	{
		// DDD Patterns comment:
		// Using a private collection field, better for DDD Aggregate's encapsulation
		// so resources cannot be added from "outside the AggregateRoot" directly to the collection,
		// but only through the method Environment.AddResource() which includes behaviour.
		private List<Resource> _resources;

		public DesiredState State { get; private set; }

		// EF comment: When using EF its typically good practice to bundle the FKEY fields with their corresponding navigation properties.
		public EnvironmentStatus Status { get; private set; }
		private int _statusId;

		public DateTime CreateDate { get; private set; }

		public string Description { get; private set; }

		public Guid OwnerId { get; private set; }

		public IEnumerable<Resource> Resources => _resources.AsReadOnly();

		protected Environment() : base()
		{
			_resources = new List<Resource>();
		}

		public Environment(Guid employeeId, string employeeName, string employeeEmail, Guid? ownerId = null) : this()
		{
			_statusId = EnvironmentStatus.Submitted.Id;
			CreateDate = DateTime.UtcNow;
			OwnerId = ownerId ?? employeeId;
			State = new DesiredState(employeeId.ToString(), employeeName, employeeEmail, string.Empty);
			
			AddDomainEvent(new EnvironmentCreated(this));
		}

		// DDD Patterns comment
		// This Environment AggregateRoot's method "AddResource()" should be the only way to add a Resource to the Environment,
		// so any behavior and validations are controlled by the AggregateRoot in order to maintain consistency between the whole Aggregate. 
		public void AddResource(Guid resourceId, string comment)
		{
			var existingResource = _resources.Where(o => o.ResourceId == resourceId).SingleOrDefault();

			if (existingResource == null)
			{        
				var resource = new Resource(resourceId, comment);

				var validationResult = resource.Validate(new ValidationContext(resource));

				if (validationResult.Any())
				{
					var innerException = new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

					throw new EnvironmentDomainException(nameof(resource), innerException);
				}

				_resources.Add(resource);
			}
		}

		public void SetInProgressStatus()
		{
			if (_statusId == EnvironmentStatus.Submitted.Id)
			{
				AddDomainEvent(new EnvironmentStatusChangedToInProgress(Id));

				_statusId = EnvironmentStatus.InProgress.Id;
				Description = "InProgress";
			}
		}

		public void SetCompletedStatus()
		{
			if (_statusId == EnvironmentStatus.InProgress.Id)
			{
				AddDomainEvent(new EnvironmentChangedToCompleted(Id));

				_statusId = EnvironmentStatus.Completed.Id;
				Description = "Completed";
			}
		}

		public void SetCancelledStatus()
		{
			if (_statusId == EnvironmentStatus.Completed.Id)
			{
				throw new EnvironmentDomainException($"{Status.Name} -> {EnvironmentStatus.Cancelled.Name}");
			}

			_statusId = EnvironmentStatus.Cancelled.Id;
			Description = "Cancelled";

			AddDomainEvent(new EnvironmentStatusChangedToCancelled(Id));
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
