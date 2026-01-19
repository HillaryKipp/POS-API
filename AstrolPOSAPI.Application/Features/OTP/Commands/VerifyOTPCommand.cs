using AstrolPOSAPI.Application.Interfaces.Repositories;
using MediatR;

namespace AstrolPOSAPI.Application.Features.OTP.Commands
{
    public class VerifyOTPCommand : IRequest<bool>
    {
        public string PhoneNumber { get; set; } = default!;
        public string OTPCode { get; set; } = default!;
        public string Purpose { get; set; } = default!;
    }

    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyOTPCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var allOTPs = await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.Identity.OTP>().GetAllAsync();

            var otp = allOTPs
                .Where(o => o.PhoneNumber == request.PhoneNumber
                    && o.OTPCode == request.OTPCode
                    && o.Purpose == request.Purpose
                    && !o.IsVerified)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            if (otp == null)
                return false;

            // Check if expired
            if (DateTime.UtcNow > otp.ExpiresAt)
                return false;

            // Mark as verified
            otp.IsVerified = true;
            otp.VerifiedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.Identity.OTP>().UpdateAsync(otp);
            await _unitOfWork.Save(cancellationToken);

            return true;
        }
    }
}
