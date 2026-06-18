using Layla.Models.DtosModels.MainDtos;

namespace Layla.Services.DynamicApartmentSearchService
{
    public interface IApartmentSearchService
    {
        Task<PagedResult<ApartmentDto>> SearchAsync(ApartmentSearchRequestDto request, CancellationToken ct);
    }
}
