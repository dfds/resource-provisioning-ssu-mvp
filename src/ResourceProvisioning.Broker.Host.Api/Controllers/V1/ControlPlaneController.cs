using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Commands.Environment;
using ResourceProvisioning.Broker.Domain.ValueObjects;

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

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var cmd = new CreateEnvironmentCommand(new DesiredState("foo", "1"));
			
			return Ok(await _broker.Handle(cmd));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] dynamic request)
		{
			IProvisioningRequest provisioningRequest = _mapper.Map<IProvisioningRequest>(request);

			var result = await _broker.Handle(provisioningRequest);

			return Ok(result);
		}
	}
}
