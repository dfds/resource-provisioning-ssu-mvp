using System;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Commands.Environment;

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
			//TODO: Finalize Get method on ControlPlaneController. (Ch3022)
			var cmd = new GetEnvironmentCommand(Guid.Empty);

			return Ok(await _broker.Handle(cmd));
		}

		//TODO: Test ControlPlaneController POST action. (Ch2139)
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] dynamic payload)
		{
			dynamic requestWrapper = new ExpandoObject();

			requestWrapper.HttpRequest = Request;
			requestWrapper.Payload = payload;

			IProvisioningRequest provisioningRequest = _mapper.Map<IProvisioningRequest>(requestWrapper);

			var result = await _broker.Handle(provisioningRequest);

			return Ok(result);
		}
	}
}
