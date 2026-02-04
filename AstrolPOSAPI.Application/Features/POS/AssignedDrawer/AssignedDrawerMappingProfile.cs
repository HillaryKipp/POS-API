using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Commands.CreateAssignedDrawer;
using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Commands.UpdateAssignedDrawer;
using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AutoMapper;
using AssignedDrawerEntity = AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer
{
    public class AssignedDrawerMappingProfile : Profile
    {
        public AssignedDrawerMappingProfile()
        {
            CreateMap<AssignedDrawerEntity, AssignedDrawerDto>().ReverseMap();
            CreateMap<CreateAssignedDrawerCommand, AssignedDrawerEntity>();
            CreateMap<UpdateAssignedDrawerCommand, AssignedDrawerEntity>();

            CreateMap<CreateAssignedDrawerDto, AssignedDrawerEntity>();
            CreateMap<UpdateAssignedDrawerDto, AssignedDrawerEntity>();
        }
    }
}
