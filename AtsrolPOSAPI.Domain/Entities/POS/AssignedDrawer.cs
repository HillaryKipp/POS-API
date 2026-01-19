using AtsrolPOSAPI.Domain.Common;
using AtsrolPOSAPI.Domain.Entities.Core;
using AtsrolPOSAPI.Domain.Entities.Identity;

namespace AtsrolPOSAPI.Domain.Entities.POS
{
    public class AssignedDrawer : BaseAuditableEntity
    {
        public string DrawerId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string? DefaultScreenId { get; set; }
        public string? DefaultShortcutBar { get; set; }
        public DateTimeOffset? SessionTimeIn { get; set; }
        public DateTimeOffset? SessionTimeOut { get; set; }
        public decimal OpenCash { get; set; }

        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        public Drawer? Drawer { get; set; }
        public AppUser? User { get; set; }
        public DefaultScreen? DefaultScreen { get; set; }
        public Core.Company? Company { get; set; }
        public Core.Store? StoreOfOperation { get; set; }
    }
}
