using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            // من DTO إلى الكيان الأساسي
            CreateMap<ReportCreateDto, Report>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // حماية من التعديل
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // من الكيان الأساسي إلى DTO للعرض
            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(src => src.Reporter!.FullName))
                .ForMember(dest => dest.ApartmentTitle, opt => opt.MapFrom(src => src.Apartment!.Title));
        }
    }
}
