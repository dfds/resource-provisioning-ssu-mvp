using System.Text.Json;
using AutoMapper;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Events.Provisioning;
using ResourceProvisioning.Broker.Application.Protocols.Http;

namespace ResourceProvisioning.Broker.Application.Mapping.Converters
{
	public class ProvisioningResponseConverter : ITypeConverter<IProvisioningResponse, IProvisioningEvent>
	{
		public IProvisioningEvent Convert(IProvisioningResponse source, IProvisioningEvent destination, ResolutionContext context)
		{
			if (source is ProvisioningBrokerResponse brokerResponse)
			{
				var getContentTask = brokerResponse.Content?.ReadAsStringAsync();

				if(getContentTask != null)
				{ 
					getContentTask.Wait();

					var reader = new Utf8JsonReader(new System.ReadOnlySpan<byte>(System.Text.Encoding.UTF8.GetBytes(getContentTask.Result)));

					if (JsonDocument.TryParseValue(ref reader, out var document))
					{
						return new ProvisioningRequestedEvent(document.RootElement);
					}
				}
			}

			throw new ProvisioningBrokerException("Unsupported IProvisioningResponse mapping");
		}
	}
}
