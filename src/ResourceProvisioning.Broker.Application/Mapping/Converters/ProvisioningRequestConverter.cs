using System.Collections.Generic;
using System.Dynamic;
using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Commands.Environment;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Application.Mapping.Converters
{
	public class ProvisioningRequestConverter : ITypeConverter<ExpandoObject, IProvisioningRequest>
	{
		public IProvisioningRequest Convert(ExpandoObject source, IProvisioningRequest destination, ResolutionContext context)
		{
			if (!source.TryAdd("HttpRequest", null) && !source.TryAdd("Payload", null))
			{
				dynamic dynamicSource = source;

				if (dynamicSource.HttpRequest.Path == "/ControlPlane" && dynamicSource.HttpRequest.Method == "POST")
				{
					if (dynamicSource.Payload is DesiredState desiredState)
					{
						return new CreateEnvironmentCommand(desiredState);
					}
				}
			}

			throw new ProvisioningBrokerException("Unsupported IProvisioningResponse mapping");
		}
	}
}
