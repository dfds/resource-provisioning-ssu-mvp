using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Broker.Domain.Core.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using RestClient.Core;
using Xunit;

namespace AcceptanceTests.Core
{
    public class SubmitDesiredStateScenario
    {
        [Fact]
        public async Task SubmitDesiredStateRecipe()
        {
            await Given_a_rest_api();
                  And_a_rest_client();
            await When_a_desired_state_is_submitted();
            await Then_it_can_be_found_in_the_environment_state();
        }

        private async Task Given_a_rest_api()
        {
            var builder = new RestApiHostBuilder();
            
            _testHost = await builder.CreateAsync();
        }

        private void And_a_rest_client()
        {
            var testHttpClient = _testHost.GetTestClient();

            _restClient = new RestClient.Core.RestClient(testHttpClient);
        }
        
        private async Task When_a_desired_state_is_submitted()
        {
            _environmentId = Guid.NewGuid();
            _desiredState = new DesiredState
            {
                Name = "grafana-x45ah",
                ApiVersion = "v1",
                Labels = new[]
                {
                    new Label
                    {
                        Key = "app-type",
                        Value = "dashboard"
                    },
                    new Label
                    {
                        Key = "owner",
                        Value = "sandbox-emcla-x7aj1"
                    }
                },
                Properties = new Dictionary<string, string>()
                {
                    {"ui-theme", "dark"},
                    {"timezone", "UTC"}
                }
            };
            
            
           await _restClient.State.SubmitDesiredStateAsync(_environmentId, _desiredState);
        }

        private async Task Then_it_can_be_found_in_the_environment_state()
        {
            var currentState = await _restClient.State
                .GetCurrentStateByEnvironmentAsync(_environmentId);
            
            Assert.Equal(
                _desiredState.Name,            
                currentState.ActualState.Single().Name
            );
            
        }

        private IHost _testHost;

        private IRestClient _restClient;
        private DesiredState _desiredState;
        private Guid _environmentId;
    }
    
}