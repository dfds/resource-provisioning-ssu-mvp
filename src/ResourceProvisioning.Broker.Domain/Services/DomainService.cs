using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using ResourceProvisioning.Broker.Repository;
using ResourceProvisioning.Broker.Service;

namespace ResourceProvisioning.Broker.Domain.Services
{
	public class DomainService : IDomainService
	{
		private readonly IEnvironmentRepository _environmentRepository;

		public DomainService(IEnvironmentRepository environmentRepository)
		{
			_environmentRepository = environmentRepository;
		}

		public Task<Aggregates.EnvironmentAggregate.Environment> AddEnvironmentAsync(DesiredState state)
		{
			throw new NotImplementedException();
		}

		public Task DeleteEnvironmentAsync(Guid environmentId)
		{
			throw new NotImplementedException();
		}

		public Task<Aggregates.EnvironmentAggregate.Environment> GetEnvironmentByIdAsync(Guid environmentId)
		{
			throw new NotImplementedException();
		}

		public Task<Aggregates.EnvironmentAggregate.Environment> UpdateEnvironmentAsync(Guid environmentId, DesiredState state)
		{
			throw new NotImplementedException();
		}
	}
}
