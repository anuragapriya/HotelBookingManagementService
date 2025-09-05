
using HotelBookingService.HotelBookingService.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> Get(string reference);
        Task AddAsync(Booking booking);
        Task<bool> IsRoomAvailable(int hotelId, int roomId, DateOnly checkIn, DateOnly checkOut);
        Task SaveChangesAsync();
    }
}
