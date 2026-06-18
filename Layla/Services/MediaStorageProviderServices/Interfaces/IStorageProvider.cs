using Layla.Models.DtosModels.ExternalMediaStorageDtos;
using static Layla.Services.MediaStorageProviderServices.Implementation.CloudinaryStorageProvider;

namespace Layla.Services.MediaStorageProviderServices.Interfaces
{
    public interface IStorageProvider
    {
        Task<UploadSignatureDto> CreateUploadSignatureAsync(int userId, int apartmentId, bool isAdmin, CancellationToken ct);
        Task <bool> DeleteAsync(int mediaId, int CurrentUserId, bool isAdmin, CancellationToken ct);
        Task<WebhookResult> ProcessWebhookAsync(HttpRequest request, CancellationToken ct);
    }
}
