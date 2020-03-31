using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Events.Integration
{
	public interface IIntegrationEventStore
	{
		Task<IEnumerable<IIntegrationEventLog>> RetrieveEventLogsPendingToPublishAsync();

		Task SaveEventAsync(IIntegrationEvent @event, DbTransaction transaction);

		Task MarkEventAsPublishedAsync(Guid eventId);

		Task MarkEventAsInProgressAsync(Guid eventId);

		Task MarkEventAsFailedAsync(Guid eventId);
	}
}
