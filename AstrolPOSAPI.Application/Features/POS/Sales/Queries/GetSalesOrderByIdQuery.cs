using AstrolPOSAPI.Application.Features.POS.Sales.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AstrolPOSAPI.Domain.Entities.POS;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.Sales.Queries
{
    public class GetSalesOrderByIdQuery : IRequest<SalesOrderDto>
    {
        public string Id { get; set; } = default!;
    }

    public class GetSalesOrderByIdQueryHandler : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSalesOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SalesOrderDto> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<SalesOrder>().GetByIdAsync(request.Id);
            if (order == null || order.DeletedDate != null)
                throw new KeyNotFoundException($"Sales order with ID {request.Id} not found");

            var allLines = await _unitOfWork.Repository<SalesOrderLine>().GetAllAsync();
            var allPayments = await _unitOfWork.Repository<Payment>().GetAllAsync();

            var dto = _mapper.Map<SalesOrderDto>(order);
            dto.Lines = _mapper.Map<List<SalesOrderLineDto>>(
                allLines.Where(l => l.SalesOrderId == request.Id && l.DeletedDate == null).ToList());
            dto.Payments = _mapper.Map<List<PaymentDto>>(
                allPayments.Where(p => p.SalesOrderId == request.Id && p.DeletedDate == null).ToList());

            return dto;
        }
    }
}
