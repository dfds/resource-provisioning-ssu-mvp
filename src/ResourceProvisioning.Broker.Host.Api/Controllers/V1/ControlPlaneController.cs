using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class ControlPlaneController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ControlPlaneController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}
	}
}
