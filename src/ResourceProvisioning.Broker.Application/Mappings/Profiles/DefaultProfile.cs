using System.Dynamic;
using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Mappings.Converters;

namespace ResourceProvisioning.Broker.Application.Mappings.Profiles
{
	public class DefaultProfile : Profile
	{
		public DefaultProfile()
		{
			CreateMap<ExpandoObject, IProvisioningRequest>().ConvertUsing<ProvisioningRequestConverter>();
			CreateMap<IProvisioningResponse, IProvisioningEvent>().ConvertUsing<ProvisioningResponseConverter>();
		}
	}
}
