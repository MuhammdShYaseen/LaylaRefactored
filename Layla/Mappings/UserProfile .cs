using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UpdateUserDto>()
                .ForMember(d => d.Email,
                    o => o.MapFrom(s => s.Email!.Value))
                .ForMember(d => d.PhoneNumber,
                    o => o.MapFrom(s => s.PhoneNumber!.Value))
                .ForMember(d => d.Lang,
                    o => o.MapFrom(s => s.Lang!.Code));

            CreateMap<UpdateUserDto, User>()
                .ForMember(d => d.Email,
                    o => o.Ignore())
                .ForMember(d => d.PhoneNumber,
                    o => o.Ignore())
                .ForMember(d => d.Lang,
                    o => o.Ignore());
        }
    }
}
