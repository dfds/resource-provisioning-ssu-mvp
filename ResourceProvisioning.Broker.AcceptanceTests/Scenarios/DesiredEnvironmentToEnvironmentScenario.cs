using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Commands.Environment;
using ResourceProvisioning.Broker.Domain.ValueObjects;
using Xunit;

namespace ResourceProvisioning.Broker.AcceptanceTests.Scenarios
{
	public class DesiredEnvironmentToEnvironmentScenario
	{
		private ServiceCollection _services;
		private IMapper _mapper;

		[Fact]
		public async Task DesiredEnvironmentToEnvironmentRecipe()
		{
			await When_a_environment_is_requested();
			Then_a_environment_is_created();
		}

		private async Task When_a_environment_is_requested()
		{
			var provider = _services.BuildServiceProvider();
			
			var provisioningBroker = provider.GetRequiredService<IProvisioningBroker>();


			var environmentDesiredState = new DesiredState(
				name: "environment",
				apiVersion: "1",
				labels: null,
				properties: new[]
				{
					new Property("name", "production"), new Property("capability.RootId", "operateports-zekzl"),
				}
			);
			var createEnvironmentCommand = new CreateEnvironmentCommand(environmentDesiredState);
			var result = await provisioningBroker.Handle(createEnvironmentCommand);
		}

		private void Then_a_environment_is_created()
		{
			throw new System.NotImplementedException();
		}

		public DesiredEnvironmentToEnvironmentScenario()
		{
			_services = new ServiceCollection();
			
			Application.DependencyInjection.AddProvisioningBroker(_services, options =>
			{
				
			});
		}
	}
}
