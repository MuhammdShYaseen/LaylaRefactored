using Layla.Models.DtosModels.ExternalServicesDtos;
using Layla.Models.GenericResponseModels;
using Layla.Services.LocationFromIPService.Interfaces;

namespace Layla.Services.LocationFromIPService.Implementations
{
    public class LocationFromIPExternalService : ILocationFromIPExternalService
    {
        private readonly HttpClient _httpClient;
        public LocationFromIPExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IpApiResponseDto?> GetAsync(string ip, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return null;

            try
            {
                var url = $"https://ip-api.com/json/{ip}";

                var response = await _httpClient
                    .GetFromJsonAsync<IpApiResponseDto>(url, ct);

                if (response?.Status != "success")
                    return null;

                return response;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) when (
                ex is HttpRequestException ||
                ex is TaskCanceledException)
            {
                return null;
            }
        }
    }
}
