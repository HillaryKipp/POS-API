using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Commands.UpdateAssignedDrawer
{
    public class UpdateAssignedDrawerCommand : IRequest<AssignedDrawerDto>
    {
        public string Id { get; set; } = default!;
        public string DrawerId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string? DefaultScreenId { get; set; }
        public string? DefaultShortcutBar { get; set; }
        public DateTimeOffset? SessionTimeIn { get; set; }
        public DateTimeOffset? SessionTimeOut { get; set; }
        public decimal OpenCash { get; set; }
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;
    }

    public class UpdateAssignedDrawerCommandValidator : AbstractValidator<UpdateAssignedDrawerCommand>
    {
        public UpdateAssignedDrawerCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.DrawerId).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.CompanyId).NotEmpty();
            RuleFor(p => p.StoreOfOperationId).NotEmpty();
            RuleFor(p => p.OpenCash).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateAssignedDrawerCommandHandler : IRequestHandler<UpdateAssignedDrawerCommand, AssignedDrawerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAssignedDrawerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignedDrawerDto> Handle(UpdateAssignedDrawerCommand request, CancellationToken cancellationToken)
        {
            var assignedDrawer = await _unitOfWork.Repository<AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>().GetByIdAsync(request.Id);

            if (assignedDrawer == null || assignedDrawer.DeletedDate != null)
                throw new KeyNotFoundException($"AssignedDrawer with ID {request.Id} not found");

            _mapper.Map(request, assignedDrawer);
            await _unitOfWork.Repository<AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>().UpdateAsync(assignedDrawer);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<AssignedDrawerDto>(assignedDrawer);
        }
    }
}
