using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResourceProvisioning.Cli.Core.Core.Models;
using ResourceProvisioning.Cli.RestShared.DataTransferObjects;

namespace ResourceProvisioning.Cli.RestClient.Core
{
    public class StateClient: IStateClient
    {
        private readonly HttpClient _httpClient;

        public StateClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnvironmentActualState> GetCurrentStateAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync(
                new Uri(RestShared.Routes.STATE, UriKind.Relative)
            );
            httpResponseMessage.EnsureSuccessStatusCode();

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            
            var actualState = JsonConvert.DeserializeObject<EnvironmentActualState>(content);

            return actualState;
        }

        public async Task<EnvironmentActualState> GetCurrentStateByEnvironmentAsync(Guid environmentId)
        {
            var route = RestShared.Routes.STATE_DASH_ENVIRONMENT_ID
                .Replace(
                    "{environmentId}",
                    environmentId.ToString()
                );
            
            
            var httpResponseMessage = await _httpClient.GetAsync(
                new Uri(route, UriKind.Relative)
            );
            httpResponseMessage.EnsureSuccessStatusCode();
            
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            
            var actualState = JsonConvert.DeserializeObject<EnvironmentActualState>(content);

            return actualState;
        }

        public Task SubmitDesiredStateAsync(Guid environmentId, DesiredState desiredState)
        {
	        throw new NotImplementedException();
        }

        public async Task SubmitDesiredStateAsync(
            Guid environmentId, 
            DesiredState desiredState
        )
        {
            var desiredStateRequest = new DesiredStateRequest
            {
                EnvironmentId = environmentId,
                DesiredState = desiredState
            };
            var payload = JsonConvert.SerializeObject(desiredStateRequest);

            var content = new StringContent(
                payload,
                Encoding.UTF8,
                "application/json"
            );

            var httpResponseMessage = await _httpClient.PostAsync(
                new Uri(RestShared.Routes.STATE, UriKind.Relative),
                content
            );
            httpResponseMessage.EnsureSuccessStatusCode();

        }

    }
}
