using AstrolPOSAPI.Application.Features.OTP.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.OTP.Commands
{
    public class SendOTPCommand : IRequest<OTPDto>
    {
        public string PhoneNumber { get; set; } = default!;
        public string Purpose { get; set; } = default!;
    }

    public class SendOTPCommandHandler : IRequestHandler<SendOTPCommand, OTPDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SendOTPCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OTPDto> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
            // Generate 6-digit OTP
            var otpCode = new Random().Next(100000, 999999).ToString();

            var otp = new AtsrolPOSAPI.Domain.Entities.Identity.OTP
            {
                PhoneNumber = request.PhoneNumber,
                OTPCode = otpCode,
                Purpose = request.Purpose,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5), // OTP expires in 5 minutes
                IsVerified = false,
                VerificationAttempts = 0
            };

            await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.Identity.OTP>().AddAsync(otp);
            await _unitOfWork.Save(cancellationToken);

            // TODO: In production, send SMS here using SMS service
            // For now, we'll just log it
            Console.WriteLine($"[OTP] Code for {request.PhoneNumber}: {otpCode} (Purpose: {request.Purpose})");

            return _mapper.Map<OTPDto>(otp);
        }
    }
}
