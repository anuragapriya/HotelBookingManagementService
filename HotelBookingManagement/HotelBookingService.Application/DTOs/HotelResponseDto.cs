using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Application.DTOs
{
    public class HotelResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<RoomResponseDto> Rooms { get; set; } = [];
    }
}
