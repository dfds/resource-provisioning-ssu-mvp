using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Domain.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public sealed class DomainService : IDomainService
	{
		private readonly IEnvironmentRepository _environmentRepository;

		public DomainService(IEnvironmentRepository environmentRepository)
		{
			_environmentRepository = environmentRepository;
		}

		public async Task<Aggregates.EnvironmentAggregate.Environment> AddEnvironmentAsync(DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = _environmentRepository.Add(new Aggregates.EnvironmentAggregate.Environment(desiredState));

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

		public async Task<Aggregates.EnvironmentAggregate.Environment> GetEnvironmentByIdAsync(Guid environmentId)
		{
			var results = await _environmentRepository.GetAsync(env => env.Id == environmentId);

			return results.SingleOrDefault();
		}

		public async Task<IEnumerable<Aggregates.EnvironmentAggregate.Environment>> GetEnvironmentByResourceIdAsync(Guid resourceId)
		{
			var results = await _environmentRepository.GetAsync((env) => env.Resources.Any(res => res.Id == resourceId));

			return results;
		}

		public async Task<Aggregates.EnvironmentAggregate.Environment> UpdateEnvironmentAsync(Guid environmentId, DesiredState desiredState, CancellationToken cancellationToken = default)
		{
			var environment = await GetEnvironmentByIdAsync(environmentId);

			environment.SetDesiredState(desiredState);

			_environmentRepository.Update(environment);

			await _environmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return environment;
		}
	}
}
