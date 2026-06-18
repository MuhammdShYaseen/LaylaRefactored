using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile() 
        {
            CreateMap<Apartment, ApartmentDto>()
            .ForMember(d => d.OwnerName,
                opt => opt.MapFrom(src => src.Owner!.FullName))

            .ForMember(d => d.MediaUrls,
                opt => opt.MapFrom(src =>
                    src.MediaFiles != null
                        ? src.MediaFiles.Select(m => m.FileUrl)
                        : Enumerable.Empty<string>()))

            .ForMember(d => d.AverageRating,
                opt => opt.MapFrom(src =>
                    src.Reviews != null && src.Reviews.Any()
                        ? src.Reviews.Average(r => r.Rating)
                        : 0))

            .ForMember(d => d.TotalReviews,
                opt => opt.MapFrom(src =>
                    src.Reviews != null
                        ? src.Reviews.Count
                        : 0));
        }
    }
}
