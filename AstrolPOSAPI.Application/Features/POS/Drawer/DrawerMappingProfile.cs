using AstrolPOSAPI.Application.Features.POS.Drawer.Commands.CreateDrawer;
using AstrolPOSAPI.Application.Features.POS.Drawer.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.Drawer
{
    public class DrawerMappingProfile : Profile
    {
        public DrawerMappingProfile()
        {
            CreateMap<AstrolPOSAPI.Domain.Entities.POS.Drawer, DrawerDto>().ReverseMap();
            CreateMap<AstrolPOSAPI.Application.Features.POS.Drawer.Commands.CreateDrawer.CreateDrawerCommand, AstrolPOSAPI.Domain.Entities.POS.Drawer>();
            CreateMap<AstrolPOSAPI.Application.Features.POS.Drawer.Commands.UpdateDrawer.UpdateDrawerCommand, AstrolPOSAPI.Domain.Entities.POS.Drawer>();
        }
    }
}
