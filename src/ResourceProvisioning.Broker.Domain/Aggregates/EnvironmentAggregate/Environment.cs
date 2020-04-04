using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Domain.Exceptions;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class Environment : BaseEntity<Guid>, IAggregateRoot
	{
		private List<EnvironmentResource> _resources;

		public DesiredState DesiredState { get; private set; }

		public EnvironmentStatus Status { get; private set; }
		private int _statusId = EnvironmentStatus.Created.Id;

		public DateTime CreateDate => DateTime.UtcNow;

		public IEnumerable<EnvironmentResource> Resources => _resources.AsReadOnly();

		private Environment() : base()
		{
			_resources = new List<EnvironmentResource>();
		}

		public Environment(DesiredState desiredState) : this()
		{
			DesiredState = desiredState;
			
			AddDomainEvent(new EnvironmentCreatedEvent(this));
		}

		public void AddResource(Guid resourceId, DateTime provisioned, string comment, bool isDesired)
		{
			var existingResource = _resources.Where(o => o.Id == resourceId).SingleOrDefault();

			if (existingResource == null)
			{        
				var resource = new EnvironmentResource(resourceId, provisioned, comment, isDesired);

				var validationResult = resource.Validate(new ValidationContext(resource));

				if (validationResult.Any())
				{
					var innerException = new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

					throw new EnvironmentDomainException(nameof(resource), innerException);
				}

				_resources.Add(resource);

				Initialize();
			}
		}

		public void SetDesiredState(DesiredState desiredState) {
			DesiredState = desiredState;

			Initialize();
		}

		public void Initialize()
		{
			_statusId = EnvironmentStatus.Initializing.Id;

			AddDomainEvent(new EnvironmentInitializingEvent(Id));
		}

		public void Start()
		{
			if (_statusId == EnvironmentStatus.Initializing.Id && HasDesiredState())
			{
				_statusId = EnvironmentStatus.Started.Id;

				AddDomainEvent(new EnvironmentStartedEvent(Id));
			}
		}

		public void Terminate()
		{
			_statusId = EnvironmentStatus.Terminated.Id;

			AddDomainEvent(new EnvironmentTerminatedEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DesiredState == null)
			{
				yield return new ValidationResult(nameof(DesiredState));
			}
		}

		private bool HasDesiredState() 
		{
			throw new NotImplementedException();
		}
	}
}
