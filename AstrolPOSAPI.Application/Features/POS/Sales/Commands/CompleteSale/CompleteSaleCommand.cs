using AstrolPOSAPI.Application.Features.POS.Sales.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AstrolPOSAPI.Domain.Entities.POS;
using AutoMapper;
using FluentValidation;
using MediatR;
using ItemEntity = AstrolPOSAPI.Domain.Entities.POS.Item;

namespace AstrolPOSAPI.Application.Features.POS.Sales.Commands.CompleteSale
{
    public class CompleteSaleCommand : IRequest<ReceiptDto>
    {
        public string SalesOrderId { get; set; } = default!;
        public bool PrintReceipt { get; set; } = true;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class CompleteSaleCommandValidator : AbstractValidator<CompleteSaleCommand>
    {
        public CompleteSaleCommandValidator()
        {
            RuleFor(p => p.SalesOrderId).NotEmpty();
        }
    }

    public class CompleteSaleCommandHandler : IRequestHandler<CompleteSaleCommand, ReceiptDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompleteSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReceiptDto> Handle(CompleteSaleCommand request, CancellationToken cancellationToken)
        {
            // Get the sales order
            var salesOrder = await _unitOfWork.Repository<SalesOrder>().GetByIdAsync(request.SalesOrderId);
            if (salesOrder == null)
                throw new KeyNotFoundException($"Sales order with ID {request.SalesOrderId} not found");

            if (salesOrder.Status != SalesOrderStatus.Pending)
                throw new InvalidOperationException("Sales order is not in pending status");

            // Verify payment is complete
            if (salesOrder.AmountPaid < salesOrder.TotalAmount)
                throw new InvalidOperationException($"Insufficient payment. Total: {salesOrder.TotalAmount:C}, Paid: {salesOrder.AmountPaid:C}");

            // Get all order lines
            var allLines = await _unitOfWork.Repository<SalesOrderLine>().GetAllAsync();
            var orderLines = allLines.Where(l => l.SalesOrderId == request.SalesOrderId && l.DeletedDate == null).ToList();

            if (!orderLines.Any())
                throw new InvalidOperationException("Cannot complete sale with no items");

            // Deduct inventory for each item
            foreach (var line in orderLines)
            {
                var item = await _unitOfWork.Repository<ItemEntity>().GetByIdAsync(line.ItemId);
                if (item != null)
                {
                    item.QuantityOnHand -= line.Quantity;
                    if (item.QuantityOnHand < 0)
                        item.QuantityOnHand = 0; // Prevent negative stock
                    await _unitOfWork.Repository<ItemEntity>().UpdateAsync(item);
                }
            }

            // Update sales order status
            salesOrder.Status = SalesOrderStatus.Completed;
            await _unitOfWork.Repository<SalesOrder>().UpdateAsync(salesOrder);

            // Generate receipt
            var receiptNo = $"RCP-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
            var receipt = new Receipt
            {
                SalesOrderId = request.SalesOrderId,
                ReceiptNo = receiptNo,
                IssuedDate = DateTime.UtcNow,
                TotalAmount = salesOrder.TotalAmount,
                AmountPaid = salesOrder.AmountPaid,
                ChangeGiven = salesOrder.ChangeGiven,
                IsPrinted = request.PrintReceipt,
                IsSentElectronically = !string.IsNullOrEmpty(request.EmailAddress) || !string.IsNullOrEmpty(request.PhoneNumber),
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber
            };

            await _unitOfWork.Repository<Receipt>().AddAsync(receipt);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<ReceiptDto>(receipt);
        }
    }
}
