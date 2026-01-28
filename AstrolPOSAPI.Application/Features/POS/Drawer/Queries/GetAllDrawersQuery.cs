using AstrolPOSAPI.Application.Features.POS.Drawer.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.Drawer.Queries
{
    public class GetAllDrawersQuery : IRequest<List<DrawerDto>>
    {
        public string? CompanyId { get; set; }
        public string? StoreOfOperationId { get; set; }
    }

    public class GetAllDrawersQueryHandler : IRequestHandler<GetAllDrawersQuery, List<DrawerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDrawersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DrawerDto>> Handle(GetAllDrawersQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Repository<AstrolPOSAPI.Domain.Entities.POS.Drawer>().GetAllAsync();
            var query = items.AsQueryable();

            if (!string.IsNullOrEmpty(request.CompanyId))
                query = query.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrEmpty(request.StoreOfOperationId))
                query = query.Where(x => x.StoreOfOperationId == request.StoreOfOperationId);

            return _mapper.Map<List<DrawerDto>>(query.ToList());
        }
    }
}
