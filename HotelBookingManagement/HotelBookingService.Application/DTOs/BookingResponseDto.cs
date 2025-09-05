using HotelBookingService.HotelBookingService.Domain.Entities;
using System;

namespace HotelBookingService.HotelBookingService.Application.DTOs
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public required string Reference { get; set; }
        public required string GuestName { get; set; }
        public int PartySize { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
