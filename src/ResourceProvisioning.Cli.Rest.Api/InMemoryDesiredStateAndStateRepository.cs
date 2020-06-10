using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourceProvisioning.Cli.Core.Core.Errors;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.Core.Core.Repositories;

namespace ResourceProvisioning.Cli.Api
{
    public class InMemoryDesiredStateAndStateRepository : IDesiredStateRepository, IStateRepository
    {
        private readonly Dictionary<Guid, List<DesiredState>> _desiredStates =
            new Dictionary<Guid, List<DesiredState>>();

        public Task StoreDesiredStateAsync(
            Guid environmentId,
            DesiredState desiredState
        )
        {
            
            if (_desiredStates.ContainsKey(environmentId) == false)
            {
                _desiredStates[environmentId] = new List<DesiredState>();
            }

            _desiredStates[environmentId].Add(desiredState);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ActualState>> GetStatesByIdAsync(Guid environmentId)
        {
            if (_desiredStates.ContainsKey(environmentId) == false) throw new EnvironmentDoesNotExistException(environmentId);

            var actualStates = _desiredStates[environmentId].Select(
                ds =>
                {
                    var state = ActualState.FromDesiredState(ds);


                    state.Status = new Status
                    {
                        Value = "initializing",
                        IsAvailable = false,
                        ReasonPhrase = "Resource is enqueued to be provisioned"
                    };


                    return state;
                });


            return Task.FromResult(actualStates);
        }
    }
}
