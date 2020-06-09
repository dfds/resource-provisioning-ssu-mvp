using System;

namespace ResourceProvisioning.Cli.Core.Core.Errors
{
    public class EnvironmentDoesNotExistException : Exception
    {
        public EnvironmentDoesNotExistException(Guid environmentId) : base(
            $"The environment with id: '{environmentId.ToString()}' does not exist"){}
    }
}
