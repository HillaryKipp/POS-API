namespace AtsrolPOSAPI.Domain.Entities.Identity
{
    /// <summary>
    /// Junction table for many-to-many relationship between Users and Stores
    /// Allows a user to be assigned to multiple stores
    /// </summary>
    public class UserStore
    {
        public string UserId { get; set; } = default!;
        public string StoreId { get; set; } = default!;

        /// <summary>
        /// Indicates if this is the user's primary/default store
        /// </summary>
        public bool IsPrimary { get; set; }

        // Navigation properties
        public AppUser User { get; set; } = default!;
        public Core.Store Store { get; set; } = default!;
    }
}
