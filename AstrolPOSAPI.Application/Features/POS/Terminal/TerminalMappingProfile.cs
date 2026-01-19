using AstrolPOSAPI.Application.Features.POS.Terminal.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.POS.Terminal
{
    public class TerminalMappingProfile : Profile
    {
        public TerminalMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.POS.Terminal, TerminalDto>().ReverseMap();
            CreateMap<CreateTerminalDto, AtsrolPOSAPI.Domain.Entities.POS.Terminal>();
            CreateMap<UpdateTerminalDto, AtsrolPOSAPI.Domain.Entities.POS.Terminal>();
        }
    }
}
