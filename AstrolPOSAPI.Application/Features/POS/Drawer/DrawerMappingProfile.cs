using AstrolPOSAPI.Application.Features.POS.Drawer.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.Drawer
{
    public class DrawerMappingProfile : Profile
    {
        public DrawerMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.Drawer, DrawerDto>().ReverseMap();
            CreateMap<CreateDrawerDto, AtsrolPOSAPI.Domain.Entities.POS.Drawer>();
            CreateMap<UpdateDrawerDto, AtsrolPOSAPI.Domain.Entities.POS.Drawer>();
        }
    }
}
