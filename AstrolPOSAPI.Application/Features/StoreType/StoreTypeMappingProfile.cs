using AstrolPOSAPI.Application.Features.StoreType.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.StoreType
{
    public class StoreTypeMappingProfile : Profile
    {
        public StoreTypeMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Core.StoreType, StoreTypeDto>();
            CreateMap<CreateStoreTypeDto, AtsrolPOSAPI.Domain.Entities.Core.StoreType>();
            CreateMap<UpdateStoreTypeDto, AtsrolPOSAPI.Domain.Entities.Core.StoreType>();
        }
    }
}
