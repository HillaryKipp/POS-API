using AstrolPOSAPI.Application.Features.POS.AssignedDrawer.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Queries
{
    public class GetAssignedDrawerByIdQuery : IRequest<AssignedDrawerDto>
    {
        public string Id { get; set; } = default!;
    }

    public class GetAssignedDrawerByIdQueryHandler : IRequestHandler<GetAssignedDrawerByIdQuery, AssignedDrawerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAssignedDrawerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignedDrawerDto> Handle(GetAssignedDrawerByIdQuery request, CancellationToken cancellationToken)
        {
            var assignedDrawer = await _unitOfWork.Repository<Domain.Entities.POS.AssignedDrawer>().GetByIdAsync(request.Id);

            if (assignedDrawer == null || assignedDrawer.DeletedDate != null)
                throw new KeyNotFoundException($"AssignedDrawer with ID {request.Id} not found");

            return _mapper.Map<AssignedDrawerDto>(assignedDrawer);
        }
    }
}
