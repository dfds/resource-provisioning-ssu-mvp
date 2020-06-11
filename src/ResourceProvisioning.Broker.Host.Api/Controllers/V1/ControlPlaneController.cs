using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[Authorize(AuthenticationSchemes = "AzureADBearer")]
	[ApiController]
	[Route("[controller]")]
	public class ControlPlaneController : ControllerBase
	{
      //
      /// Disabled due to it not working out of the box, and fixing it isn't in the scope of Story #2612 - CLI: login via OpenId
      //    
//		private readonly IMediator _mediator;

		public ControlPlaneController()
		{
      //
      /// Disabled due to it not working out of the box, and fixing it isn't in the scope of Story #2612 - CLI: login via OpenId
      //
//			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}
		
		[HttpGet("")]
		public async Task<IActionResult> Get()
		{
			return Ok();
		}
		
	}
}
