using System;

namespace ResourceProvisioning.Cli.Core.Core.Models
{
    public class ActualState : DesiredState
    {
        public string CreatedTimestamp { get; set; }
        public string LastUpdatedTimestamp { get; set; }
        public Status Status { get; set; }
        public ResourcePrincipal ResourcePrincipal { get; set; }

        public static ActualState FromDesiredState(DesiredState desiredState)
        {
            var actualState = new ActualState
            {
                Name = desiredState.Name,
                ApiVersion = desiredState.ApiVersion,
                Labels = desiredState.Labels,
                Properties = desiredState.Properties
            };

            var nowUtc = DateTime.UtcNow.ToString("u");
            actualState.CreatedTimestamp = nowUtc;
            actualState.LastUpdatedTimestamp = nowUtc;

            return actualState;
        }
    }
}
