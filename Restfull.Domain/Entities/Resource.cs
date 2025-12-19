using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restfull.Domain.Enums;

namespace Restfull.Domain.Entities
{
    public abstract class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ResourceStatus Status { get; set; }

        // Связь с Supplier
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; } = null!;

        // Связь с Location
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; } = null!;

        public virtual void ShowInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}, Status: {Status}");
        }

        public abstract void ShowDetails();
    }
}