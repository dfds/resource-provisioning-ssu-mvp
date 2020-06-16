using System;
using System.Threading.Tasks;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Infrastructure.Idempotency
{
	public sealed class RequestManager : IRequestManager
	{
		private readonly DomainContext _context;

		public RequestManager(DomainContext context)
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
				throw new Exception($"Request with {id} already exists") :
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
