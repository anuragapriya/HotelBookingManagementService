using System;

namespace HotelBookingService.HotelBookingService.Application.DTOs
{
    public class BookingRequestDto
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public int PartySize { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
    }
}
