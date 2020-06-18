using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Broker.Domain.Events.Environment;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Aggregates.Environment
{
	public sealed class EnvironmentRoot : Entity<Guid>, IAggregateRoot
	{
		private List<EnvironmentResourceReference> _resources;

		public DesiredState DesiredState { get; private set; }

		public EnvironmentStatus Status { get; private set; }
		private int _statusId;

		public DateTime CreateDate { get; private set; }

		public IEnumerable<EnvironmentResourceReference> Resources => _resources.AsReadOnly();

		private EnvironmentRoot()
		{
			CreateDate = DateTime.Now;
			_statusId = EnvironmentStatus.Requested.Id;
			_resources = new List<EnvironmentResourceReference>();
			Status = EnvironmentStatus.Requested;
			AddDomainEvent(new EnvironmentRequestedEvent(this));
		}

		public EnvironmentRoot(DesiredState desiredState) : this()
		{
			SetDesiredState(desiredState);
		}

		public void AddResource(Guid resourceId, DateTime provisioned, string comment)
		{
			var existingResource = _resources.SingleOrDefault(o => o.ResourceId == resourceId);

			if (existingResource != null) { return; }


			var resource = new EnvironmentResourceReference(resourceId, provisioned, comment);

			var validationResult = resource.Validate(new ValidationContext(resource));

			if (validationResult.Any())
			{
				var innerException =
					new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

				throw new ProvisioningBrokerDomainException(nameof(resource), innerException);
			}

			_resources.Add(resource);

			Initialize();
		}

		public void SetDesiredState(DesiredState desiredState)
		{
			DesiredState = desiredState;

			Initialize();
		}

		public void Initialize()
		{
			_statusId = GridActorStatus.Initializing.Id;

			AddDomainEvent(new EnvironmentInitializingEvent(Id));
		}

		public void Created()
		{
			if (_statusId != GridActorStatus.Initializing.Id)
			{
				return;
			}

			_statusId = EnvironmentStatus.Created.Id;

			AddDomainEvent(new EnvironmentCreatedEvent(Id));
		}

		public void Terminated()
		{
			_statusId = GridActorStatus.Terminated.Id;

			AddDomainEvent(new EnvironmentTerminatedEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DesiredState == null)
			{
				yield return new ValidationResult(nameof(DesiredState));
			}
		}
	}
}
