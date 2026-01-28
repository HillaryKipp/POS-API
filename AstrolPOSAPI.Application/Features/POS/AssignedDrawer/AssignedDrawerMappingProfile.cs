using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer
{
    public class AssignedDrawerMappingProfile : Profile
    {
        public AssignedDrawerMappingProfile()
        {
            CreateMap<AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer, AssignedDrawerDto>().ReverseMap();
            CreateMap<CreateAssignedDrawerDto, AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>();
            CreateMap<UpdateAssignedDrawerDto, AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>();
        }
    }
}
