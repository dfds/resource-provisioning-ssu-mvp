//using System;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using Moq.AutoMock;
//using ResourceProvisioning.Cli.Application;
//using ResourceProvisioning.Cli.Application.Models;
//using ResourceProvisioning.Cli.Infrastructure.Net.Http;
//using Xunit;

//namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
//{
//	public class ProgramCommandCanPipeToApplyScenario
//    {
//        private IBrokerClient _restClient;
//        private Guid _environmentId;
//        private DesiredState _payload;

//        [Fact]
//        public async Task SubmitDesiredStateToBroker()
//        {
//            await Given_a_json_payload();
//            await And_a_environment_id();
//            await And_a_rest_client();
//            await When_terminal_input_apply_and_json();
//            await Then_rest_client_posts_provisioning_request_to_broker();
//        }

//        private async Task Given_a_json_payload()
//        {
//            var jsonPayload = "{\"Name\":null,\"ApiVersion\":\"1\",\"Labels\":null,\"Properties\":null}";

//            _payload = JsonSerializer.Deserialize<DesiredState>(jsonPayload);
//        }

//        private async Task And_a_environment_id()
//        {
//            _environmentId = Guid.NewGuid();
//        }

//        private async Task And_a_rest_client()
//        {
//            var mocker = new AutoMocker();
//			var restClientMock = mocker.GetMock<IBrokerClient>();

//			restClientMock.Setup(o => o.ApplyDesiredStateAsync( It.IsAny<Guid>(), It.IsAny<DesiredState>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            
//            _restClient = restClientMock.Object;
//        }

//        private async Task When_terminal_input_apply_and_json()
//        {
//            var serviceCollection = new ServiceCollection();

//            serviceCollection.AddTransient(factory => _restClient);

//            Program.RuntimeServices = serviceCollection;

//            await Program.Main("apply", JsonSerializer.Serialize(_payload), $"-e={_environmentId.ToString()}");
//        }

//        private async Task Then_rest_client_posts_provisioning_request_to_broker()
//        {
//            _stateClientMock.Verify(mock => mock.SubmitDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>()), Times.Once());
//        }
//    }
    
//}
