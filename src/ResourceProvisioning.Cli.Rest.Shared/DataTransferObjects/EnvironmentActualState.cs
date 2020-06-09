using System;
using System.Collections.Generic;
using ResourceProvisioning.Cli.Core.Core.Models;

namespace ResourceProvisioning.Cli.RestShared.DataTransferObjects
{
    public class EnvironmentActualState
    {
        public Guid EnvironmentId { get; set; }
        public IEnumerable<ActualState> ActualState { get; set; }
    }
}
