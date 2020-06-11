using System;
using System.Text.Json;
using System.Threading.Tasks;
using Datadog.Trace;
using Datadog.Trace.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.AutoMock;
using ResourceProvisioning.Cli.Application;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.RestClient.Core;
using Xunit;

namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
{
    public class ProgramCommandCanPipeToApplyScenario
    {
        private IRestClient _restClient;
        private Guid _environmentId;
        private Tracer _ddTracer;
        private DesiredState _payload;
        private Mock<IStateClient> _stateClientMock;

        [Fact]
        public async Task SubmitDesiredStateToBroker()
        {
            await Given_a_json_payload();
            await And_a_environment_id();
            await And_a_rest_client();
            await And_a_datadog_tracer();
            await When_terminal_input_apply_and_json();
            await Then_rest_client_posts_provisioning_request_to_broker();
        }

        private async Task Given_a_json_payload()
        {
            var jsonPayload = "{\"Name\":null,\"ApiVersion\":\"1\",\"Labels\":null,\"Properties\":null}";

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

        private async Task And_a_datadog_tracer()
        {
            // read default configuration sources (env vars, web.config, datadog.json)
            var settings = TracerSettings.FromDefaultSources();

            // change some settings
            settings.ServiceName = "SsuCli";

            //COMMENT: For this to work run the following command: "kubectl port-forward -n datadog datadog-agent-27rs2 8126"
            settings.AgentUri = new Uri("http://localhost:8126/");

            settings.GlobalTags.Add("rune_er_for_sej", "ja_det_er_han");

            // enable da fuq integrations for DD
            settings.Integrations["HttpMessageHandler"].Enabled = true;

            // create a new Tracer using these settings
            _ddTracer = new Tracer(settings);
        }

        private async Task When_terminal_input_apply_and_json()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient(factory => _restClient);
            serviceCollection.AddTransient(factory => _ddTracer);

            Program.RuntimeServices = serviceCollection;

            await Program.Main("apply", JsonSerializer.Serialize(_payload), $"-e={_environmentId.ToString()}");
        }

        private async Task Then_rest_client_posts_provisioning_request_to_broker()
        {
            _stateClientMock.Verify(mock => mock.SubmitDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>()), Times.Once());
        }
    }
    
}
