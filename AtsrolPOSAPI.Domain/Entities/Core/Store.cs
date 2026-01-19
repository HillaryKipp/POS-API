using AtsrolPOSAPI.Domain.Common;

namespace AtsrolPOSAPI.Domain.Entities.Core
{
    public class Store : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        // Required relationship to Company
        public string CompanyId { get; set; } = default!;

        // Optional relationship to StoreType
        public string? StoreTypeId { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation properties
        public Company? Company { get; set; }
        public StoreType? StoreType { get; set; }
    }
}
