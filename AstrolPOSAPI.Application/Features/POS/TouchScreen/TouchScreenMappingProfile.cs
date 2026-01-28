using AstrolPOSAPI.Application.Features.POS.TouchScreen.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.TouchScreen
{
    public class TouchScreenMappingProfile : Profile
    {
        public TouchScreenMappingProfile()
        {
            CreateMap<AstrolPOSAPI.Domain.Entities.POS.TouchScreen, TouchScreenDto>().ReverseMap();
            CreateMap<AstrolPOSAPI.Domain.Entities.POS.TouchScreenButton, TouchScreenButtonDto>().ReverseMap();
            CreateMap<CreateTouchScreenDto, AstrolPOSAPI.Domain.Entities.POS.TouchScreen>();
            CreateMap<UpdateTouchScreenDto, AstrolPOSAPI.Domain.Entities.POS.TouchScreen>();
            CreateMap<CreateTouchScreenButtonDto, AstrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
            CreateMap<UpdateTouchScreenButtonDto, AstrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
            
            CreateMap<AstrolPOSAPI.Application.Features.POS.TouchScreen.Commands.CreateTouchScreen.CreateTouchScreenCommand, AstrolPOSAPI.Domain.Entities.POS.TouchScreen>();
            CreateMap<AstrolPOSAPI.Application.Features.POS.TouchScreen.Commands.UpdateTouchScreen.UpdateTouchScreenCommand, AstrolPOSAPI.Domain.Entities.POS.TouchScreen>();

            // Add Command mappings for TouchScreenButton if missing elsewhere
            CreateMap<AstrolPOSAPI.Application.Features.POS.TouchScreenButton.Commands.CreateTouchScreenButton.CreateTouchScreenButtonCommand, AstrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
            CreateMap<AstrolPOSAPI.Application.Features.POS.TouchScreenButton.Commands.UpdateTouchScreenButton.UpdateTouchScreenButtonCommand, AstrolPOSAPI.Domain.Entities.POS.TouchScreenButton>();
        }
    }
}
