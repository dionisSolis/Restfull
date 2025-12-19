using System.ComponentModel.DataAnnotations;

namespace Restfull.Domain.Entities
{
    public class Furniture : Resource
    {
        [StringLength(50)]
        public string Material { get; set; } = string.Empty;

        [StringLength(100)]
        public string Dimensions { get; set; } = string.Empty;

        public override void ShowDetails()
        {
            Console.WriteLine($"Furniture Details - Material: {Material}, Dimensions: {Dimensions}");
        }
    }
}