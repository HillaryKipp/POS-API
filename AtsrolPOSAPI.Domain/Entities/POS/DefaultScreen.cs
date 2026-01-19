using AtsrolPOSAPI.Domain.Common;
using AtsrolPOSAPI.Domain.Entities.Core;

namespace AtsrolPOSAPI.Domain.Entities.POS
{
    public class DefaultScreen : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        public Core.Company? Company { get; set; }
        public Core.Store? StoreOfOperation { get; set; }
    }
}
