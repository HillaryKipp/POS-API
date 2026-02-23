using AstrolPOSAPI.Application.Features.Accounting.PostingGroups.DTOs;
using AstrolPOSAPI.Domain.Entities.Accounting;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.Accounting.PostingGroups
{
    public class PostingGroupMappingProfile : Profile
    {
        public PostingGroupMappingProfile()
        {
            CreateMap<VendorPostingGroup, VendorPostingGroupDto>();
            CreateMap<CreateVendorPostingGroupDto, VendorPostingGroup>();
            CreateMap<UpdateVendorPostingGroupDto, VendorPostingGroup>();

            CreateMap<GenBusPostingGroup, GenBusPostingGroupDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Description));
            CreateMap<CreateGenBusPostingGroupDto, GenBusPostingGroup>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdateGenBusPostingGroupDto, GenBusPostingGroup>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));
        }
    }
}
