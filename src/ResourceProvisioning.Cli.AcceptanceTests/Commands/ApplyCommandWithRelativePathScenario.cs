using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.AutoMock;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Host.Console;
using Xunit;

namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
{
	public class ApplyCommandWithRelativePathScenario
	{
		private Mock<IBrokerService> _brokerServiceMock;
		private Guid _environmentId;

		[Fact]
		public async Task SubmitDesiredStateToBroker()
		{
			await Given_an_environment_id();
			await And_a_rest_client();
			await When_apply_is_given_a_relative_path();
			await Then_rest_client_posts_provisioning_request_to_broker();
		}

		private async Task Given_an_environment_id()
		{
			_environmentId = Guid.NewGuid();
		}

		private async Task And_a_rest_client()
		{
			var mocker = new AutoMocker();
			_brokerServiceMock = mocker.GetMock<IBrokerService>();

			_brokerServiceMock.Setup(o => o.ApplyDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>(), It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);
		}

		private async Task When_apply_is_given_a_relative_path()
		{
			var serviceCollection = new ServiceCollection();

			serviceCollection.AddTransient(factory => _brokerServiceMock.Object);

			Program.RuntimeServices = serviceCollection;

			var manifestPath = Path.Combine(
				@"Commands/TestMaterial",
				"single-resource-manifest.json"
			);

			await Program.Main(
				"apply",
				manifestPath,
				$"-e={_environmentId.ToString()}"
			);
		}

		private async Task Then_rest_client_posts_provisioning_request_to_broker()
		{
			_brokerServiceMock.Verify(mock => mock.ApplyDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>(), It.IsAny<CancellationToken>()),
				Times.Once());
		}
	}
}
