using AstrolPOSAPI.Application.Features.POS.DrawerGroup.Commands.CreateDrawerGroup;
using AstrolPOSAPI.Application.Features.POS.DrawerGroup.Commands.UpdateDrawerGroup;
using AstrolPOSAPI.Application.Features.POS.DrawerGroup.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.DrawerGroup
{
    public class DrawerGroupMappingProfile : Profile
    {
        public DrawerGroupMappingProfile()
        {
            CreateMap<Domain.Entities.POS.DrawerGroup, DrawerGroupDto>().ReverseMap();
            CreateMap<CreateDrawerGroupDto, Domain.Entities.POS.DrawerGroup>();
            CreateMap<UpdateDrawerGroupDto, Domain.Entities.POS.DrawerGroup>();
            CreateMap<CreateDrawerGroupCommand, Domain.Entities.POS.DrawerGroup>();
            CreateMap<UpdateDrawerGroupCommand, Domain.Entities.POS.DrawerGroup>();
        }
    }
}
