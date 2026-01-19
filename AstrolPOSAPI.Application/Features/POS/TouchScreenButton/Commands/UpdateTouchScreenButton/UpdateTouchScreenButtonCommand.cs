using AstrolPOSAPI.Application.Features.POS.TouchScreen.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AtsrolPOSAPI.Domain.Entities.POS;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.TouchScreenButton.Commands.UpdateTouchScreenButton
{
    public class UpdateTouchScreenButtonCommand : IRequest<TouchScreenButtonDto>
    {
        public string Id { get; set; } = default!;
        public string TouchScreenId { get; set; } = default!;
        public ButtonType ButtonType { get; set; }
        public string? ItemId { get; set; }
        public string ItemName { get; set; } = default!;
        public ButtonShape Shape { get; set; }
        public string BackgroundColor { get; set; } = default!;
        public string TextColor { get; set; } = default!;
        public int? FontSize { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public bool ShowImage { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefaultImage { get; set; }
        public int SortOrder { get; set; }
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;
    }

    public class UpdateTouchScreenButtonCommandValidator : AbstractValidator<UpdateTouchScreenButtonCommand>
    {
        public UpdateTouchScreenButtonCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.TouchScreenId).NotEmpty();
            RuleFor(p => p.ItemName).NotEmpty().MaximumLength(100);
            RuleFor(p => p.BackgroundColor).Matches(@"^#[0-9A-Fa-f]{6}$");
            RuleFor(p => p.TextColor).Matches(@"^#[0-9A-Fa-f]{6}$");
            RuleFor(p => p.FontSize).InclusiveBetween(8, 72).When(p => p.FontSize.HasValue);
            RuleFor(p => p.Row).GreaterThan(0);
            RuleFor(p => p.Column).GreaterThan(0);
            RuleFor(p => p.RowSpan).InclusiveBetween(1, 10);
            RuleFor(p => p.ColumnSpan).InclusiveBetween(1, 10);
            RuleFor(p => p.CompanyId).NotEmpty();
            RuleFor(p => p.StoreOfOperationId).NotEmpty();
        }
    }

    public class UpdateTouchScreenButtonCommandHandler : IRequestHandler<UpdateTouchScreenButtonCommand, TouchScreenButtonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTouchScreenButtonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TouchScreenButtonDto> Handle(UpdateTouchScreenButtonCommand request, CancellationToken cancellationToken)
        {
            var button = await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.POS.TouchScreenButton>().GetByIdAsync(request.Id);

            if (button == null || button.DeletedDate != null)
                throw new KeyNotFoundException($"TouchScreenButton with ID {request.Id} not found");

            _mapper.Map(request, button);
            await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.POS.TouchScreenButton>().UpdateAsync(button);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<TouchScreenButtonDto>(button);
        }
    }
}
