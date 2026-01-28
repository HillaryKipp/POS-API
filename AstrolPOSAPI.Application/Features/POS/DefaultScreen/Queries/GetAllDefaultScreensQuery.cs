using AstrolPOSAPI.Application.Features.POS.DefaultScreen.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.DefaultScreen.Queries
{
    public class GetAllDefaultScreensQuery : IRequest<List<DefaultScreenDto>>
    {
        public string? CompanyId { get; set; }
        public string? StoreOfOperationId { get; set; }
    }

    public class GetAllDefaultScreensQueryHandler : IRequestHandler<GetAllDefaultScreensQuery, List<DefaultScreenDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDefaultScreensQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DefaultScreenDto>> Handle(GetAllDefaultScreensQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Repository<Domain.Entities.POS.DefaultScreen>().GetAllAsync();
            var query = items.AsQueryable();

            if (!string.IsNullOrEmpty(request.CompanyId))
                query = query.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrEmpty(request.StoreOfOperationId))
                query = query.Where(x => x.StoreOfOperationId == request.StoreOfOperationId);

            return _mapper.Map<List<DefaultScreenDto>>(query.ToList());
        }
    }
}
