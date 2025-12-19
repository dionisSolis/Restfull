using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restfull.Domain.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room? Room { get; set; }
    }
}