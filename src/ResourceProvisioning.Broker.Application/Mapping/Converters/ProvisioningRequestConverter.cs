using System.Dynamic;
using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Mapping.Converters
{
	//TODO: Map http request payload (dynamic - json) to IProvisioningRequest (Ch3022)
	public class ProvisioningRequestConverter : ITypeConverter<ExpandoObject, IProvisioningRequest>
	{
		public IProvisioningRequest Convert(ExpandoObject source, IProvisioningRequest destination, ResolutionContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}
