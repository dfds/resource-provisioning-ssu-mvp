using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Broker.Domain.Events.Environment;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Aggregates.Resource
{
	public sealed class ResourceRoot : Entity<Guid>, IAggregateRoot
	{
		public ResourceStatus Status { get; private set; }
		private int _statusId;

		public DesiredState DesiredState { get; private set; }

		public DateTime RegisteredDate { get; private set; } = DateTime.Now;

		private ResourceRoot() : base()
		{
			_statusId = GridActorStatus.Initializing.Id;

			AddDomainEvent(new ResourceInitializingEvent(this));
		}

		public ResourceRoot(DesiredState desiredState) : base()
		{
			DesiredState = desiredState;
		}

		public void SetDesiredState(DesiredState desiredState)
		{
			DesiredState = desiredState;
		}

		public void Ready()
		{
			_statusId = ResourceStatus.Ready.Id;

			AddDomainEvent(new ResourceReadyEvent(Id));
		}

		public void Unavailable()
		{
			_statusId = ResourceStatus.Unavailable.Id;

			AddDomainEvent(new ResourceUnavailableEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Status == null)
			{
				yield return new ValidationResult(nameof(Status));
			}
		}
	}
}
