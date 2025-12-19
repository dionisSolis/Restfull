using System.ComponentModel.DataAnnotations;
using Restfull.Domain.Enums;

namespace Restfull.Domain.Entities
{
    public class Room : Resource
    {
        [StringLength(20)]
        public string Number { get; set; } = string.Empty;

        [StringLength(50)]
        public string Building { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public override void ShowDetails()
        {
            Console.WriteLine($"Room Details - Number: {Number}, Building: {Building}, " +
                            $"Capacity: {Capacity}, Type: {RoomType}");
        }
    }
}