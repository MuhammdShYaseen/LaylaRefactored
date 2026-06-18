using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            // من DTO → إلى الكيان الأساسي
            CreateMap<ReviewCreateDto, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // من الكيان → قراءة DTO
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ApartmentTitle, opt => opt.MapFrom(src => src.Apartment!.Title))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User!.FullName));
        }
    }
}
