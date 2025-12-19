using System.ComponentModel.DataAnnotations;

namespace Restfull.Domain.Entities
{
    public class Equipment : Resource
    {
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [StringLength(100)]
        public string SerialNumber { get; set; } = string.Empty;

        public override void ShowDetails()
        {
            Console.WriteLine($"Equipment Details - Model: {Model}, Serial Number: {SerialNumber}");
        }
    }
}