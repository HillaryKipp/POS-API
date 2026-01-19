using AtsrolPOSAPI.Domain.Common;

namespace AtsrolPOSAPI.Domain.Entities.Audit
{
    public class AuditLog : BaseEntity
    {
        public string TableName { get; set; } = default!;
        public string KeyValues { get; set; } = default!; // JSON string of key values
        public string? OldValues { get; set; } // JSON
        public string? NewValues { get; set; } // JSON
        public string Action { get; set; } = default!; // Insert/Update/Delete
        public string? UserId { get; set; }
        public DateTimeOffset OccurredAt { get; set; }
        public string? CorrelationId { get; set; }
        public string? RequestPath { get; set; }
    }
}
