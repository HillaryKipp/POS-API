using System.ComponentModel.DataAnnotations;
using AtsrolPOSAPI.Domain.Entities.POS;

namespace AstrolPOSAPI.Application.Features.POS.Drawer.DTOs
{
    public class DrawerDto
    {
        public string Id { get; set; } = default!;
        public string DrawerGroupId { get; set; } = default!;
        public string? DefaultScreenId { get; set; }
        public string TerminalId { get; set; } = default!;
        public DrawerStatus Status { get; set; }
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        // Optional: include related entity names/codes if needed
    }

    public class CreateDrawerDto
    {
        [Required]
        public string DrawerGroupId { get; set; } = default!;

        public string? DefaultScreenId { get; set; }

        [Required]
        public string TerminalId { get; set; } = default!;

        [Required]
        public DrawerStatus Status { get; set; }

        [Required]
        public string CompanyId { get; set; } = default!;

        [Required]
        public string StoreOfOperationId { get; set; } = default!;
    }

    public class UpdateDrawerDto
    {
        [Required]
        public string Id { get; set; } = default!;

        [Required]
        public string DrawerGroupId { get; set; } = default!;

        public string? DefaultScreenId { get; set; }

        [Required]
        public string TerminalId { get; set; } = default!;

        [Required]
        public DrawerStatus Status { get; set; }

        [Required]
        public string CompanyId { get; set; } = default!;

        [Required]
        public string StoreOfOperationId { get; set; } = default!;
    }
}
