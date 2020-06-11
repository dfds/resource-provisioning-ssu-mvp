using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Cli.Core.Core.Repositories;
using ResourceProvisioning.Cli.RestShared;
using ResourceProvisioning.Cli.RestShared.DataTransferObjects;

namespace ResourceProvisioning.Cli.Api.Controllers
{
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IDesiredStateRepository _desiredStateRepository;
        private readonly IStateRepository _stateRepository;

        public StateController(
            IDesiredStateRepository desiredStateRepository,
            IStateRepository stateRepository
        )
        {
            _desiredStateRepository = desiredStateRepository;
            _stateRepository = stateRepository;
        }

        [HttpGet(Routes.STATE)]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException("Use Desired state for query");
            return Ok();
        }

        [HttpGet(Routes.STATE_DASH_ENVIRONMENT_ID)]
        public async Task<IActionResult> Get([FromRoute] Guid environmentId)
        {
            var states = await _stateRepository.GetStatesByIdAsync(environmentId);
            
            var environmentActualState = new EnvironmentActualState
            {
                EnvironmentId = environmentId,
                ActualState = states
            };
            
            return Ok(environmentActualState);
        }

        [HttpPost(Routes.STATE)]
        public async Task<IActionResult> Post([FromBody] DesiredStateRequest request)
        {
            await _desiredStateRepository.StoreDesiredStateAsync(
                request.EnvironmentId,
                request.DesiredState
            );

            return Ok();
        }
    }
}
