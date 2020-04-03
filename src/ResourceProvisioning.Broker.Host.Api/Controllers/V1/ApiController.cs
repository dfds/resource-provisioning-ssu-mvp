using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class ApiController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ApiController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}
	}
}
