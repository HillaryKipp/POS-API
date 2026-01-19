using AstrolPOSAPI.Application.Features

.Store.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.Store
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Core.Store, StoreDto>();
            CreateMap<CreateStoreDto, AtsrolPOSAPI.Domain.Entities.Core.Store>();
            CreateMap<UpdateStoreDto, AtsrolPOSAPI.Domain.Entities.Core.Store>();
        }
    }
}
