using AstrolPOSAPI.Application.Features.GeneralSettings.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.GeneralSettings
{
    public class GeneralSettingsMappingProfile : Profile
    {
        public GeneralSettingsMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Core.GeneralSettings, GeneralSettingsDto>();
            CreateMap<CreateGeneralSettingsDto, AtsrolPOSAPI.Domain.Entities.Core.GeneralSettings>();
            CreateMap<UpdateGeneralSettingsDto, AtsrolPOSAPI.Domain.Entities.Core.GeneralSettings>();
        }
    }
}
