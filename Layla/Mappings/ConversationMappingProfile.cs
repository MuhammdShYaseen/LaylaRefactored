using AutoMapper;
using Layla.Models.DtosModels.ConversationDtos;
using Layla.Models.DtosModels.MessageDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public sealed class ConversationMappingProfile : Profile
    {
        public ConversationMappingProfile()
        {
            CreateMap<Conversation, ConversationDto>()
                .ForMember(d => d.ApartmentTitle,
                    opt => opt.MapFrom(s => s.Apartment.Title))

                .ForMember(d => d.ApartmentImageUrl,
                    opt => opt.MapFrom(s =>
                    s.Apartment.MediaFiles
                    .Where(m => m.IsPrimary)
                    .Select(m => m.FileUrl)
                    .FirstOrDefault()))

                .ForMember(d => d.UserName,
                    opt => opt.MapFrom(s => s.User.FullName))

                .ForMember(d => d.OwnerName,
                    opt => opt.MapFrom(s => s.Apartment.Owner.FullName))

                .ForMember(d => d.LastMessage,
                    opt => opt.MapFrom(s =>
                        s.Messages!
                         .OrderByDescending(m => m.CreatedAt)
                         .Select(m => m.Content)
                         .FirstOrDefault()))

                .ForMember(d => d.LastMessageAt,
                    opt => opt.MapFrom(s =>
                        s.Messages!
                         .Max(m => (DateTime?)m.CreatedAt)))

                .ForMember(d => d.UnreadCount,
                    opt => opt.Ignore());

            CreateMap<Conversation, ConversationDetailsDto>()
                .ForMember(d => d.ApartmentTitle,
                    opt => opt.MapFrom(s => s.Apartment.Title))

                .ForMember(d => d.Messages,
                    opt => opt.MapFrom(s =>
                        s.Messages!
                         .OrderBy(m => m.CreatedAt)));
        }
    }
}
