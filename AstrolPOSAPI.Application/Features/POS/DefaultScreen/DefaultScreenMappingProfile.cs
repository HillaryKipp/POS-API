using AstrolPOSAPI.Application.Features.POS.DefaultScreen.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.DefaultScreen
{
    public class DefaultScreenMappingProfile : Profile
    {
        public DefaultScreenMappingProfile()
        {
            CreateMap<AstrolPOSAPI.Domain.Entities.POS.DefaultScreen, DefaultScreenDto>().ReverseMap();
            CreateMap<CreateDefaultScreenDto, AstrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
            CreateMap<UpdateDefaultScreenDto, AstrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
            
            CreateMap<AstrolPOSAPI.Application.Features.POS.DefaultScreen.Commands.CreateDefaultScreen.CreateDefaultScreenCommand, AstrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
            CreateMap<AstrolPOSAPI.Application.Features.POS.DefaultScreen.Commands.UpdateDefaultScreen.UpdateDefaultScreenCommand, AstrolPOSAPI.Domain.Entities.POS.DefaultScreen>();
        }
    }
}
