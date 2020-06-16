using System.Dynamic;
using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Mapping.Converters;

namespace ResourceProvisioning.Broker.Application.Mapping.Profiles
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
