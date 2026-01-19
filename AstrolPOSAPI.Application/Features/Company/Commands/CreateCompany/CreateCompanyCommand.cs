using AutoMapper;
using AstrolPOSAPI.Application.Features.Company.DTOs;
using AstrolPOSAPI.Application.Interfaces.Repositories;
using AstrolPOSAPI.Application.Interfaces.Services;
using MediatR;

namespace AstrolPOSAPI.Application.Features.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<CompanyDto>
    {
        // Code is auto-generated, not provided by user
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INoSeriesService _noSeriesService;

        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INoSeriesService noSeriesService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _noSeriesService = noSeriesService;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            // Auto-generate Company Code using NoSeries
            var code = await _noSeriesService.GenerateNextNumberAsync("COMPANY", cancellationToken);

            var company = new AtsrolPOSAPI.Domain.Entities.Core.Company
            {
                Code = code,
                Name = request.Name,
                Description = request.Description
            };

            await _unitOfWork.Repository<AtsrolPOSAPI.Domain.Entities.Core.Company>().AddAsync(company);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<CompanyDto>(company);
        }
    }
}
