using Layla.Models.DtosModels.ExternalServicesDtos;

namespace Layla.Services.LocationFromIPService.Interfaces
{
    public interface ILocationFromIPExternalService
    {
        Task <IpApiResponseDto?> GetAsync (string ip, CancellationToken ct = default);
    }
}
