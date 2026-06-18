using AutoMapper;
using Layla.Models.DtosModels.MainDtos;
using Layla.Models.MainModels;

namespace Layla.Mappings
{
    public class ContractProfile : Profile
    {
        public ContractProfile() 
        {
            CreateMap<Contract, ContractDto>();

            CreateMap<CreateContractDto, Contract>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsSignedByOwner, opt => opt.Ignore())
                .ForMember(dest => dest.IsSignedByRenter, opt => opt.Ignore());
        }
    }
}
