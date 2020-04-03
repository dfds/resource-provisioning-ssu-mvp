using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Broker.Application.Models;

namespace ResourceProvisioning.Broker.Application.Queries
{
	public class EnvironmentQueries : IQueryProvider
	{
		private readonly IMaterializedViewProvider _materializedViewProvider;

		public EnvironmentQueries(IMaterializedViewProvider materializedViewProvider)
		{
			//TODO: Implement materialized view provider
			_materializedViewProvider = materializedViewProvider ?? throw new ArgumentException(nameof(materializedViewProvider));
		}

		public async Task<EnvironmentViewModel> GetEnvironmentById(Guid environmentId)
		{
			return await Query<EnvironmentViewModel>(environmentId);
		}

		public async Task<EnvironmentViewModel> GetEnvironmentByEnvironmentResourceId(Guid resourceId)
		{
			return await Query<EnvironmentViewModel>(resourceId);
		}

		public async Task<EnvironmentResourceViewModel> GetEnvironmentResourceById(Guid resourceId)
		{
			return await Query<EnvironmentResourceViewModel>(resourceId);
		}

		public async Task<T> Query<T>(params object[] args) where T : class, IMaterializedView
		{
			return await _materializedViewProvider.MaterializeAsync(args) as T;
		}
	}
}
