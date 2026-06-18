using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class MediaFileProfile : Profile
    {
        public MediaFileProfile() 
        {
            CreateMap<MediaFile, MediaFileDto>();

            CreateMap<MediaFileCreateDto, MediaFile>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
