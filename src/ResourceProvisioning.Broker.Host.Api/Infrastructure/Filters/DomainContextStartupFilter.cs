using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ResourceProvisioning.Broker.Host.Api.Infrastructure.Filters
{
	public class DomainContextStartupFilter : IStartupFilter
	{
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			throw new NotImplementedException();
		}
	}
}
