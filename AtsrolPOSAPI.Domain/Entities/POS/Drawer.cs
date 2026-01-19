using AtsrolPOSAPI.Domain.Common;
using AtsrolPOSAPI.Domain.Entities.Core;

namespace AtsrolPOSAPI.Domain.Entities.POS
{
    public enum DrawerStatus
    {
        Available = 0,
        Assigned = 1,
        Closed = 2
    }

    public class Drawer : BaseAuditableEntity
    {
        public string DrawerGroupId { get; set; } = default!;
        public string? DefaultScreenId { get; set; }
        public string TerminalId { get; set; } = default!;
        public DrawerStatus Status { get; set; }

        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        public DrawerGroup? DrawerGroup { get; set; }
        public DefaultScreen? DefaultScreen { get; set; }
        public Terminal? Terminal { get; set; }
        public Core.Company? Company { get; set; }
        public Core.Store? StoreOfOperation { get; set; }
    }
}
