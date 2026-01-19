using AtsrolPOSAPI.Domain.Common;

namespace AtsrolPOSAPI.Domain.Entities.Identity
{
    public class Permission : BaseAuditableEntity
    {
        // Either RoleId or UserId should be set (or both for specific overrides)
        public string? RoleId { get; set; }
        public string? UserId { get; set; }

        // Resource that this permission applies to (e.g., "Company", "Store", "User", etc.)
        public string ResourceName { get; set; } = default!;

        // Permission flags
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        // Navigation properties
        public AppRole? Role { get; set; }
        public AppUser? User { get; set; }
    }
}
