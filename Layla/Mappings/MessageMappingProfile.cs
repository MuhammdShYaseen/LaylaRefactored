using AutoMapper;
using Layla.Models.DtosModels.MessageDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public sealed class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(
                    dest => dest.VoiceUrl,
                    opt => opt.MapFrom(src =>
                        src.VoiceFilePath == null
                            ? null
                            : $"/api/messages/voice/{src.Id}"
                    )
                );
        }
    }
}
