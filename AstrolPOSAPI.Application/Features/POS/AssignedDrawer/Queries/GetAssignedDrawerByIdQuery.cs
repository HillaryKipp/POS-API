using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Queries
{
    public class GetAllAssignedDrawersQuery : IRequest<List<AssignedDrawerDto>>
    {
        public string? CompanyId { get; set; }
        public string? StoreOfOperationId { get; set; }
        public string? UserId { get; set; }
        public string? DrawerId { get; set; }
    }

    public class GetAllAssignedDrawersQueryHandler : IRequestHandler<GetAllAssignedDrawersQuery, List<AssignedDrawerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAssignedDrawersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AssignedDrawerDto>> Handle(GetAllAssignedDrawersQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Repository<Domain.Entities.POS.AssignedDrawer>().GetAllAsync();
            var query = items.AsQueryable();

            if (!string.IsNullOrEmpty(request.CompanyId))
                query = query.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrEmpty(request.StoreOfOperationId))
                query = query.Where(x => x.StoreOfOperationId == request.StoreOfOperationId);

            if (!string.IsNullOrEmpty(request.UserId))
                query = query.Where(x => x.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.DrawerId))
                query = query.Where(x => x.DrawerId == request.DrawerId);

            return _mapper.Map<List<AssignedDrawerDto>>(query.ToList());
        }
    }
}
