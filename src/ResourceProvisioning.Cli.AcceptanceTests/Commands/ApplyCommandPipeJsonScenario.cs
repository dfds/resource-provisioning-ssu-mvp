using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using ResourceProvisioning.Cli.Application.Commands;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Repositories;
using Xunit;

namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
{
	public class ApplyCommandPipeJsonScenario
	{
		private Mock<IBrokerService> _brokerClientMock;
		private Apply _applyCommand;
		private Guid _environmentId;
		private DesiredState _payload;

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

			_brokerClientMock = mocker.GetMock<IBrokerService>();

			_brokerClientMock.Setup(o => o.ApplyDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
		}

		private async Task When_create_an_apply_command()
		{
			_applyCommand = new Apply(_brokerClientMock.Object, new ManifestRepository<DesiredState>()) { DesiredStateSource = JsonSerializer.Serialize(_payload), EnvironmentId = _environmentId.ToString() };
		}

		private async Task Then_rest_client_posts_provisioning_request_to_broker()
		{
			await _applyCommand.OnExecuteAsync();

			_brokerClientMock.Verify(mock => mock.ApplyDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>(), It.IsAny<CancellationToken>()), Times.Once());
		}
	}

}
