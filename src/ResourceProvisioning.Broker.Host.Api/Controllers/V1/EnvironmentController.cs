using System;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class EnvironmentController : ControllerBase
	{
		private readonly IProvisioningBroker _broker;

		public EnvironmentController(IProvisioningBroker broker)
		{
			_broker = broker ?? throw new ArgumentNullException(nameof(broker));
		}
	}
}
