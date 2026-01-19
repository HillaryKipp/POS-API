using AtsrolPOSAPI.Domain.Common;
using AtsrolPOSAPI.Domain.Entities.Core;

namespace AtsrolPOSAPI.Domain.Entities.POS
{
    public enum ButtonType
    {
        Item = 0,
        Category = 1,
        Function = 2,
        Modifier = 3
    }

    public enum ButtonShape
    {
        Rectangle = 0,
        Circle = 1,
        Square = 2,
        RoundedRectangle = 3
    }

    public class TouchScreen : BaseAuditableEntity
    {
        public string ScreenName { get; set; } = default!;
        public string? Description { get; set; }
        public int GridRows { get; set; } = 2;
        public int GridColumns { get; set; } = 2;
        public int DefaultFontSize { get; set; } = 12;

        // Multi-tenancy
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        // Navigation properties
        public Company? Company { get; set; }
        public Store? StoreOfOperation { get; set; }
        public ICollection<TouchScreenButton> Buttons { get; set; } = new List<TouchScreenButton>();
    }
}
