using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class ControlPlaneController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public ControlPlaneController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok("Hello world");
		}
		
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] dynamic request)
		{
			var command = _mapper.Map<dynamic, IProvisioningRequest>(request);
			
			await _mediator.Send(command);

			return Ok();
		}
	}
}
