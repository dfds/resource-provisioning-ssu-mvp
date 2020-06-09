using System.Collections.Generic;

namespace ResourceProvisioning.Cli.Core.Core.Models
{
    public class DesiredState
    {
        public string Name { get; set; }
        public string ApiVersion { get; set; }
        public IEnumerable<Label> Labels { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
