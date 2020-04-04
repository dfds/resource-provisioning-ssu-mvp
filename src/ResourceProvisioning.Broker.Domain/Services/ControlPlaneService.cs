using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Domain.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate;
using ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate;

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

		public async Task<EnvironmentRoot> AddEnvironmentAsync(DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = _environmentRepository.Add(new EnvironmentRoot(desiredState));

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

		public async Task<EnvironmentRoot> UpdateEnvironmentAsync(Guid environmentId, DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = await GetEnvironmentByIdAsync(environmentId);

			environment.SetDesiredState(desiredState);

			_environmentRepository.Update(environment);

			await _environmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return environment;
		}

		public async Task<ResourceRoot> AddResourceAsync(DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var resource = _resourceRepository.Add(new ResourceRoot(desiredState));

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
		
		public async Task<ResourceRoot> UpdateResourceAsync(Guid resourceId, DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var resource = await GetResourceByIdAsync(resourceId);

			resource.SetDesiredState(desiredState);

			_resourceRepository.Update(resource);

			await _resourceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return resource;
		}
	}
}
