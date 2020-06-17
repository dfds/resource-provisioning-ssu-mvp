using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Domain.Aggregates.Environment;
using ResourceProvisioning.Broker.Domain.Aggregates.Resource;
using ResourceProvisioning.Broker.Domain.Repository;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public sealed class ControlPlaneService : IControlPlaneService
	{
		private readonly IEnvironmentRepository _environmentRepository;
		private readonly IResourceRepository _resourceRepository;

		public ControlPlaneService(IEnvironmentRepository environmentRepository, IResourceRepository resourceRepository)
		{
			_environmentRepository = environmentRepository;
			_resourceRepository = resourceRepository;
		}

		public async Task<EnvironmentRoot> AddEnvironmentAsync(IDesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = _environmentRepository.Add(new EnvironmentRoot((DesiredState)desiredState));

			await _environmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return environment;
		}

		public async Task DeleteEnvironmentAsync(Guid environmentId, CancellationToken cancellationToken = default)
		{
			var environment = await GetEnvironmentByIdAsync(environmentId);

			if (environment != null)
			{
				_environmentRepository.Delete(environment);

				await _environmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task<EnvironmentRoot> GetEnvironmentByIdAsync(Guid environmentId)
		{
			var results = await _environmentRepository.GetAsync(env => env.Id == environmentId);

			return results.SingleOrDefault();
		}

		public async Task<IEnumerable<EnvironmentRoot>> GetEnvironmentByResourceIdAsync(Guid resourceId)
		{
			var results = await _environmentRepository.GetAsync((env) => env.Resources.Any(res => res.Id == resourceId));

			return results;
		}

		public async Task<EnvironmentRoot> UpdateEnvironmentAsync(Guid environmentId, IDesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = await GetEnvironmentByIdAsync(environmentId);

			environment.SetDesiredState((DesiredState)desiredState);

			_environmentRepository.Update(environment);

			await _environmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return environment;
		}

		public async Task<ResourceRoot> AddResourceAsync(IDesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var resource = _resourceRepository.Add(new ResourceRoot((DesiredState)desiredState));

			await _resourceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return resource;
		}

		public async Task DeleteResourceAsync(Guid resourceId, CancellationToken cancellationToken = default)
		{
			var resource = await GetResourceByIdAsync(resourceId);

			if (resource != null)
			{
				_resourceRepository.Delete(resource);

				await _resourceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task<ResourceRoot> GetResourceByIdAsync(Guid resourceId)
		{
			var results = await _resourceRepository.GetAsync(env => env.Id == resourceId);

			return results.SingleOrDefault();
		}

		public async Task<ResourceRoot> UpdateResourceAsync(Guid resourceId, IDesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var resource = await GetResourceByIdAsync(resourceId);

			resource.SetDesiredState((DesiredState)desiredState);

			_resourceRepository.Update(resource);

			await _resourceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return resource;
		}

		public async Task<IEnumerable<IAggregateRoot>> GetAggregatesByState(IDesiredState desiredState)
		{
			var resources = await _resourceRepository.GetAsync(o => o.DesiredState.Equals(desiredState));
			var environments = await _environmentRepository.GetAsync(o => o.DesiredState.Equals(desiredState));

			return resources.Cast<IAggregateRoot>().Concat(environments);
		}

		public Task<IEnumerable<EnvironmentRoot>> GetEnvironmentsAsync()
		{
			return _environmentRepository.GetAsync(o => true);
		}
	}
}
