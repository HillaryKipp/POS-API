using Microsoft.AspNetCore.Identity;

namespace AtsrolPOSAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole
    {
        // Extend role fields if required later
        public string? Description { get; set; }
    }
}
