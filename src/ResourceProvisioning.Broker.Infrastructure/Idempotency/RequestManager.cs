using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Infrastructure.Exceptions;

namespace ResourceProvisioning.Broker.Infrastructure.Idempotency
{
	public class RequestManager : IRequestManager
	{
		private readonly DomainDbContext _context;

		public RequestManager(DomainDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<bool> ExistAsync(Guid id)
		{
			return await _context.FindAsync<ClientRequest>(id) != null;
		}

		public async Task CreateRequestForCommandAsync<T>(Guid id)
		{ 
			var exists = await ExistAsync(id);

			var request = exists ? 
				throw new ContextProcessingInfrastructureException($"Request with {id} already exists") : 
				new ClientRequest()
				{
					Id = id,
					Name = typeof(T).Name,
					Time = DateTime.UtcNow
				};

			_context.Add(request);

			await _context.SaveChangesAsync();
		}
	}
}
