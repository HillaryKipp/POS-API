using AtsrolPOSAPI.Domain.Common;
using AtsrolPOSAPI.Domain.Entities.Core;

namespace AtsrolPOSAPI.Domain.Entities.POS
{
    public class TouchScreenButton : BaseAuditableEntity
    {
        public string TouchScreenId { get; set; } = default!;
        public ButtonType ButtonType { get; set; }

        // Reference to actual item/category if applicable
        public string? ItemId { get; set; }

        // Display properties
        public string ItemName { get; set; } = default!;
        public ButtonShape Shape { get; set; } = ButtonShape.Rectangle;
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string TextColor { get; set; } = "#000000";
        public int? FontSize { get; set; } // Null means use screen's DefaultFontSize

        // Grid position
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; } = 1;
        public int ColumnSpan { get; set; } = 1;

        // Image properties
        public bool ShowImage { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefaultImage { get; set; }

        public int SortOrder { get; set; } // For ordering within same cell

        // Multi-tenancy
        public string CompanyId { get; set; } = default!;
        public string StoreOfOperationId { get; set; } = default!;

        // Navigation properties
        public TouchScreen? TouchScreen { get; set; }
        public Company? Company { get; set; }
        public Store? StoreOfOperation { get; set; }
    }
}
