using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer
{
    public class AssignedDrawerMappingProfile : Profile
    {
        public AssignedDrawerMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.AssignedDrawer, AssignedDrawerDto>().ReverseMap();
            CreateMap<CreateAssignedDrawerDto, AtsrolPOSAPI.Domain.Entities.POS.AssignedDrawer>();
            CreateMap<UpdateAssignedDrawerDto, AtsrolPOSAPI.Domain.Entities.POS.AssignedDrawer>();
        }
    }
}
