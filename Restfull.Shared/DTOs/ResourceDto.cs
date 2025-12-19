namespace Restfull.Shared.DTOs
{
    public class ResourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string LocationDescription { get; set; } = string.Empty;

        // Software
        public string? Version { get; set; }
        public string? LicenseKey { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }

        // Equipment
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }

        // Furniture
        public string? Material { get; set; }
        public string? Dimensions { get; set; }

        // Room
        public string? Number { get; set; }
        public string? Building { get; set; }
        public int? Capacity { get; set; }
        public string? RoomType { get; set; }
    }

    public class CreateResourceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Equipment"; // Добавил это поле!
        public int Status { get; set; } = 0;
        public int SupplierId { get; set; }
        public int LocationId { get; set; }

        // Software
        public string? Version { get; set; }
        public string? LicenseKey { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }

        // Equipment
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }

        // Furniture
        public string? Material { get; set; }
        public string? Dimensions { get; set; }

        // Room
        public string? Number { get; set; }
        public string? Building { get; set; }
        public int? Capacity { get; set; }
        public int? RoomType { get; set; }
    }
}