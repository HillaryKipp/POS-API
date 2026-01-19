namespace AstrolPOSAPI.Application.Features.Store.DTOs
{
    public class StoreDto
    {
        public string Id { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string CompanyId { get; set; } = default!;
        public string? StoreTypeId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateStoreDto
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string CompanyId { get; set; } = default!;
        public string? StoreTypeId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdateStoreDto
    {
        public string Id { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string CompanyId { get; set; } = default!;
        public string? StoreTypeId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
