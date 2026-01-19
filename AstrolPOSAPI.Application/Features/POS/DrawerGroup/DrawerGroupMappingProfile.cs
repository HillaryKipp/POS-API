using AstrolPOSAPI.Application.Features.POS.DrawerGroup.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.DrawerGroup
{
    public class DrawerGroupMappingProfile : Profile
    {
        public DrawerGroupMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.DrawerGroup, DrawerGroupDto>().ReverseMap();
            CreateMap<CreateDrawerGroupDto, AtsrolPOSAPI.Domain.Entities.POS.DrawerGroup>();
            CreateMap<UpdateDrawerGroupDto, AtsrolPOSAPI.Domain.Entities.POS.DrawerGroup>();
        }
    }
}
