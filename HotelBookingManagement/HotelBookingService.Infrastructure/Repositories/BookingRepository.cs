using System;
using System.Threading.Tasks;
using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using HotelBookingService.HotelBookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingService.HotelBookingService.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _db;
        public BookingRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Booking booking)
        {
            await _db.Bookings.AddAsync(booking);
        }

        public async Task<Booking?> Get(string reference)
        {
            return await _db.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Reference == reference);
        }

        public async Task<bool> IsRoomAvailable(int hotelId,int roomId, DateOnly checkIn, DateOnly checkOut)
        {
            return !await _db.Bookings.AnyAsync(b =>b.HotelId==hotelId && b.RoomId == roomId && checkIn <= b.CheckOut  && b.CheckIn <= checkOut);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
