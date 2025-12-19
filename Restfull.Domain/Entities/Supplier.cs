using System.ComponentModel.DataAnnotations;

namespace Restfull.Domain.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string ContactInfo { get; set; } = string.Empty;
    }
}