using AstrolPOSAPI.Application.Features.OTP.DTOs;
using AutoMapper;

namespace AstrolPOSAPI.Application.Features.OTP
{
    public class OTPMappingProfile : Profile
    {
        public OTPMappingProfile()
        {
            CreateMap<AtsrolPOSAPI.Domain.Entities.Identity.OTP, OTPDto>();
        }
    }
}
