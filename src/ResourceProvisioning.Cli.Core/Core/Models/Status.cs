namespace ResourceProvisioning.Cli.Core.Core.Models
{
    public class Status
    {
        public string Value { get; set; }
        public bool IsAvailable { get; set; }
        public string ReasonPhrase { get; set; }
        public string ReasonUri { get; set; }
    }
}
