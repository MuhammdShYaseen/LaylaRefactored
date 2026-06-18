using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class DeviceTokenProfile : Profile
    {
        public DeviceTokenProfile()
        {
            CreateMap<DeviceToken, DeviceTokenDto>();

            CreateMap<DeviceTokenDto, DeviceToken>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.LastSeenAt, opt => opt.MapFrom(src => src.LastSeenAt ?? DateTime.UtcNow))
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
