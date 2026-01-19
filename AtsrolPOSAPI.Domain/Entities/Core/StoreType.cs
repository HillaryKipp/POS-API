using AtsrolPOSAPI.Domain.Common;

namespace AtsrolPOSAPI.Domain.Entities.Core
{
    public class StoreType : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasOtp { get; set; }
        public string? CompanyId { get; set; }
    }
}
