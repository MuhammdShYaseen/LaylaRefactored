using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;
using AutoMapper;

namespace Layla.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.ApartmentTitle,
                           opt => opt.MapFrom(src => src.Apartment!.Title))
                .ForMember(d => d.ApartmentAddress,
                           opt => opt.MapFrom(src => src.Apartment!.Location!.ToString()))
                .ForMember(d => d.UserFullName,
                           opt => opt.MapFrom(src => src.User!.FullName));

            CreateMap<CreateBookingDto, Booking>();
            CreateMap<CreateBookingDto, Booking>().ReverseMap();
            CreateMap<BookingDto, Booking>().ReverseMap();
        }
    }
}
