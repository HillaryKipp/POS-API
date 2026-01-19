using AstrolPOSAPI.Application.Features.NoSeries.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.NoSeries
{
    public class NoSeriesMappingProfile : Profile
    {
        public NoSeriesMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Core.NoSeries, NoSeriesDto>();
            CreateMap<CreateNoSeriesDto, AtsrolPOSAPI.Domain.Entities.Core.NoSeries>();
            CreateMap<UpdateNoSeriesDto, AtsrolPOSAPI.Domain.Entities.Core.NoSeries>();
        }
    }
}
