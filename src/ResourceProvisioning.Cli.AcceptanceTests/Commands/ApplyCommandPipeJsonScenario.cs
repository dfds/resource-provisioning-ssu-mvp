using System;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using ResourceProvisioning.Cli.Application.Commands;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.RestClient.Core;
using Xunit;

namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
{
    public class ApplyCommandPipeJsonScenario
    {
        private IRestClient _restClient;
        private Apply _applyCommand;
        private Guid _environmentId;
        private DesiredState _payload;
        private Mock<IStateClient> _stateClientMock;

        [Fact]
        public async Task SubmitDesiredStateToBroker()
        {
            await Given_a_json_payload();
            await And_a_environment_id();
            await And_a_rest_client();
            await When_create_an_apply_command();
            await Then_rest_client_posts_provisioning_request_to_broker();
            
        }

        private async Task Given_a_json_payload()
        {
            var jsonPayload = "{\"Name\":null,\"ApiVersion\":null,\"Labels\":null,\"Properties\":null}";

            _payload = JsonSerializer.Deserialize<DesiredState>(jsonPayload);
        }

        private async Task And_a_environment_id()
        {
            _environmentId = Guid.NewGuid();
        }

        private async Task And_a_rest_client()
        {
            var mocker = new AutoMocker();
            _stateClientMock = mocker.GetMock<IStateClient>();

            _stateClientMock.Setup(o => o.SubmitDesiredStateAsync( It.IsAny<Guid>(), It.IsAny<DesiredState>())).Returns(Task.CompletedTask);
            
            var restClientMock = mocker.GetMock<IRestClient>();

            restClientMock.Setup(o => o.State).Returns(_stateClientMock.Object);

            _restClient = restClientMock.Object;
        }

        private async Task When_create_an_apply_command()
        {
            _applyCommand = new Apply(_restClient) { DesiredStateSource = JsonSerializer.Serialize(_payload), EnvironmentId = _environmentId.ToString() };
        }

        private async Task Then_rest_client_posts_provisioning_request_to_broker()
        {
            await _applyCommand.OnExecuteAsync();

            _stateClientMock.Verify(mock => mock.SubmitDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>()), Times.Once());
        }
    }
    
}
