using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class ControlPlaneController : ControllerBase
	{
		private readonly IProvisioningBroker _broker;
		private readonly IMapper _mapper;

		public ControlPlaneController(IProvisioningBroker broker, IMapper mapper)
		{
			_broker = broker ?? throw new ArgumentNullException(nameof(broker));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] dynamic request)
		{
			//TODO: Implement automapper maps / profiles.
			var provisioningRequest = _mapper.Map<dynamic, IProvisioningRequest>(request);

			var result = await _broker.Handle(provisioningRequest);

			return Ok(result);
		}
	}
}
