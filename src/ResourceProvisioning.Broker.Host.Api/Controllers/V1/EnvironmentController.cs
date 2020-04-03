using System;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class EnvironmentController : ControllerBase
	{
		private readonly IProvisioningBroker _broker;
		private readonly IQueryProvider _queryProvider;

		public EnvironmentController(IProvisioningBroker broker, IQueryProvider queryProvider)
		{
			_broker = broker ?? throw new ArgumentNullException(nameof(broker));
			_queryProvider = queryProvider ?? throw new ArgumentNullException(nameof(queryProvider));
		}
	}
}
