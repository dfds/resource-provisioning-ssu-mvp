using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application.Mapping.Converters
{
	//TODO: Map IProvisioningResponse to IProvisioningEvent (Ch2139)
	public class ProvisioningResponseConverter : ITypeConverter<IProvisioningResponse, IProvisioningEvent>
	{
		public IProvisioningEvent Convert(IProvisioningResponse source, IProvisioningEvent destination, ResolutionContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}
