using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Services.DataCRUD.Interfaces
{
    public interface IMediaFileService
    {
        Task<IEnumerable<MediaFileDto>> GetByApartmentIdAsync(int apartmentId);
        Task<MediaFile?> GetByIdAsync(int id);
        Task<List<MediaFile>> UploadFilesAsync(int apartmentId, List<IFormFile> files, int UserId, bool isAdmin);
        Task<bool> DeleteAsync(int mediaId, int userId, bool isAdmin);
        
    }
}
