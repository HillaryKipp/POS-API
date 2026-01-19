using AstrolPOSAPI.Application.Features.POS.TouchScreen.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.TouchScreen
{
    public class TouchScreenMappingProfile : Profile
    {
        public TouchScreenMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.TouchScreen, TouchScreenDto>().ReverseMap();
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.TouchScreenButton, TouchScreenButtonDto>().ReverseMap();
            CreateMap<CreateTouchScreenDto, AtsrolPOSAPI.Domain.Entities.POS.TouchScreen>();
            CreateMap<UpdateTouchScreenDto, AtsrolPOSAPI.Domain.Entities.POS.TouchScreen>();
            CreateMap<CreateTouchScreenButtonDto, AtsrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
            CreateMap<UpdateTouchScreenButtonDto, AtsrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
        }
    }
}
