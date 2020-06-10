using System;
using ResourceProvisioning.Cli.Core.Core.Models;

namespace ResourceProvisioning.Cli.RestShared.DataTransferObjects
{
    public class DesiredStateRequest
    {
        public Guid EnvironmentId { get; set; }
        public DesiredState DesiredState { get; set; }
    }
}
