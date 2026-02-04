using AstrolPOSAPI.Application.Features.POS.Sales.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AstrolPOSAPI.Domain.Entities.POS;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.Sales.Commands.CreateSalesOrder
{
    public class CreateSalesOrderCommand : IRequest<SalesOrderDto>
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string CashierId { get; set; } = default!;
        public string DrawerId { get; set; } = default!;
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;
    }

    public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
    {
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(p => p.CashierId).NotEmpty();
            RuleFor(p => p.DrawerId).NotEmpty();
            RuleFor(p => p.CompanyId).NotEmpty();
            RuleFor(p => p.StoreOfOperationId).NotEmpty();
        }
    }

    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, SalesOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSalesOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SalesOrderDto> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            // Generate order number
            var orderNo = $"SO-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

            var salesOrder = new SalesOrder
            {
                OrderNo = orderNo,
                OrderDate = DateTime.UtcNow,
                Status = SalesOrderStatus.Pending,
                CustomerId = request.CustomerId,
                CustomerName = request.CustomerName,
                CashierId = request.CashierId,
                DrawerId = request.DrawerId,
                CompanyId = request.CompanyId,
                StoreOfOperationId = request.StoreOfOperationId,
                Subtotal = 0,
                DiscountAmount = 0,
                TaxAmount = 0,
                TotalAmount = 0
            };

            await _unitOfWork.Repository<SalesOrder>().AddAsync(salesOrder);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}
