using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;
using System.Linq.Expressions;

namespace Layla.Services.DynamicApartmentSearchService.BuilderServices
{
    public interface IApartmentFilterBuilder
    {
        IQueryable<Apartment> Build(IQueryable<Apartment> query, ApartmentSearchRequestDto request);
    }
}
