using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Commands.CreateAssignedDrawer
{
    public class CreateAssignedDrawerCommand : IRequest<AssignedDrawerDto>
    {
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

    public class CreateAssignedDrawerCommandValidator : AbstractValidator<CreateAssignedDrawerCommand>
    {
        public CreateAssignedDrawerCommandValidator()
        {
            RuleFor(p => p.DrawerId).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.CompanyId).NotEmpty();
            RuleFor(p => p.StoreOfOperationId).NotEmpty();
            RuleFor(p => p.OpenCash).GreaterThanOrEqualTo(0);
        }
    }

    public class CreateAssignedDrawerCommandHandler : IRequestHandler<CreateAssignedDrawerCommand, AssignedDrawerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssignedDrawerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignedDrawerDto> Handle(CreateAssignedDrawerCommand request, CancellationToken cancellationToken)
        {
            var assignedDrawer = _mapper.Map<AtsrolPOSAPI.Domain.Entities.POS.AssignedDrawer>(request);
            await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.POS.AssignedDrawer>().AddAsync(assignedDrawer);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<AssignedDrawerDto>(assignedDrawer);
        }
    }
}
