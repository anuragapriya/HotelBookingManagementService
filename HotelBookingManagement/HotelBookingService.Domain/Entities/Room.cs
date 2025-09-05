
namespace HotelBookingService.HotelBookingService.Domain.Entities
{
    public enum RoomType { Single=1, Double=2, Deluxe=3 }

    public enum RoomStatus { Available=1, Booked=2 }
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public required string Number { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; } = [];
    }
}
