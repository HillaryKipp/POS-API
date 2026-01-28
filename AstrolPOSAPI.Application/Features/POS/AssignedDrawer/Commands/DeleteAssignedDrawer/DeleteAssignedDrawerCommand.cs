using AstrolPOSAPI.Application.Interfaces.Repositories;
using MediatR;

namespace AstrolPOSAPI.Application.Features.POS.AssignedDrawer.Commands.DeleteAssignedDrawer
{
    public class DeleteAssignedDrawerCommand : IRequest<bool>
    {
        public string Id { get; set; } = default!;
    }

    public class DeleteAssignedDrawerCommandHandler : IRequestHandler<DeleteAssignedDrawerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssignedDrawerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAssignedDrawerCommand request, CancellationToken cancellationToken)
        {
            var assignedDrawer = await _unitOfWork.Repository<AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>().GetByIdAsync(request.Id);

            if (assignedDrawer == null)
                throw new KeyNotFoundException($"AssignedDrawer with ID {request.Id} not found");

            assignedDrawer.DeletedDate = DateTime.UtcNow;
            await _unitOfWork.Repository<AstrolPOSAPI.Domain.Entities.POS.AssignedDrawer>().UpdateAsync(assignedDrawer);
            await _unitOfWork.Save(cancellationToken);

            return true;
        }
    }
}
