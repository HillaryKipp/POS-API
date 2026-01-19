using AutoMapper;
using AstrolPOSAPI.Application.Features.Company.DTOs;
using AtsrolPOSAPI.Domain.Entities.Core;

namespace AstrolPOSAPI.Application.Features.Company
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Core.Company, CompanyDto>();
            CreateMap<CreateCompanyDto, AtsrolPOSAPI.Domain.Entities.Core.Company>();
            CreateMap<UpdateCompanyDto, AtsrolPOSAPI.Domain.Entities.Core.Company>();
        }
    }
}
