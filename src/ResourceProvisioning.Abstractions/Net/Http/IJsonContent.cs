using System.Text.Json;
using System.Threading.Tasks;

namespace ResourceProvisioning.Abstractions.Net.Http
{
	public interface IJsonContent
	{
		JsonDocument Document { get; }

		Task<byte[]> ReadAsByteArrayAsync();
	}
}
