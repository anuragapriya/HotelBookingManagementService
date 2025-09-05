using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Application.DTOs
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public required string Number { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public int Capacity { get; set; }
        public List<BookingResponseDto> Bookings { get; set; } = [];
    }
}
