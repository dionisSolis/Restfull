namespace Restfull.Shared.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? RoomId { get; set; }
    }
}