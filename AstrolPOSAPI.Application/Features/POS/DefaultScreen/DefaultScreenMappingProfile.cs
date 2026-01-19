using AstrolPOSAPI.Application.Features.POS.DefaultScreen.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.DefaultScreen
{
    public class DefaultScreenMappingProfile : Profile
    {
        public DefaultScreenMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.DefaultScreen, DefaultScreenDto>().ReverseMap();
            CreateMap<CreateDefaultScreenDto, AtsrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
            CreateMap<UpdateDefaultScreenDto, AtsrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
        }
    }
}
