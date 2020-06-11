﻿//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using Moq.AutoMock;
//using ResourceProvisioning.Cli.Application;
//using ResourceProvisioning.Cli.Core.Core.Models;
//using ResourceProvisioning.Cli.RestClient.Core;
//using Xunit;

//namespace ResourceProvisioning.Cli.AcceptanceTests.Commands
//{
//    public class ApplyCommandWithRelativePathToFolderScenario
//    {
//        [Fact]
//        public async Task SubmitDesiredStateToBroker()
//        {
//            await Given_an_environment_id();
//            await And_a_rest_client();
//            await When_apply_is_given_a_relative_path();
//            await Then_rest_client_posts_provisioning_request_to_broker();
//        }

//        private async Task Given_an_environment_id()
//        {
//            _environmentId = Guid.Parse("10ec8d7c-fd7c-4071-898c-de447208d3ac");
            
//        }

//        private async Task And_a_rest_client()
//        {
//            var mocker = new AutoMocker();
//            _stateClientMock = mocker.GetMock<IStateClient>();

//            _stateClientMock.Setup(o => o.SubmitDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>()))
//                .Returns(Task.CompletedTask);

//            var restClientMock = mocker.GetMock<IRestClient>();

//            restClientMock.Setup(o => o.State).Returns(_stateClientMock.Object);

//            _restClient = restClientMock.Object;
//        }

//        private async Task When_apply_is_given_a_relative_path()
//        {
//            var serviceCollection = new ServiceCollection();

//            serviceCollection.AddTransient(factory => _restClient);

//            Program.RuntimeServices = serviceCollection;

//            var manifestPath = Path.Combine(
//                @"Commands/TestMaterial"
//            );


//            await Program.Main(
//                "apply",
//                manifestPath,
//                $"-e={_environmentId.ToString()}"
//            );
//        }

//        private async Task Then_rest_client_posts_provisioning_request_to_broker()
//        {
//            _stateClientMock.Verify(mock => 
//                mock.SubmitDesiredStateAsync(It.IsAny<Guid>(), It.IsAny<DesiredState>()),
//                Times.Exactly(2)
//            );
//        }

//        private IRestClient _restClient;
//        private Mock<IStateClient> _stateClientMock;
//        private Guid _environmentId;
//    }
//}
