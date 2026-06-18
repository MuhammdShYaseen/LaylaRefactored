using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Services.DataCRUD.Interfaces
{
    public interface IDeviceTokenService
    {
        Task<IEnumerable<DeviceTokenDto>> GetByUserIdAsync(int userId, CancellationToken ct);
        Task<DeviceTokenDto> UpsertAsync(DeviceTokenUpsertDto dto, int currentUserId, CancellationToken ct);
        Task<bool> DeleteAsync(int id, CancellationToken ct);
        Task CleanupInactiveAsync(TimeSpan maxAge);
    }
}
