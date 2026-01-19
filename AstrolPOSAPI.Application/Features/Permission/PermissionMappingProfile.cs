using AstrolPOSAPI.Application.Features.Permission.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.Permission
{
    public class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Identity.Permission, PermissionDto>();
            CreateMap<CreatePermissionDto, AtsrolPOSAPI.Domain.Entities.Identity.Permission>();
            CreateMap<UpdatePermissionDto, AtsrolPOSAPI.Domain.Entities.Identity.Permission>();
        }
    }
}
