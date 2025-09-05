using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using HotelBookingService.HotelBookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingService.HotelBookingService.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _db;
        public RoomRepository(AppDbContext db) { _db = db; }

        public async Task<List<Room>> Get(int hotelId) =>
            await _db.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
        public async Task<Room?> Get(int hotelId, int roomId) =>
            await _db.Rooms.Include(r => r.Bookings).FirstOrDefaultAsync(r => r.HotelId==hotelId && r.Id == roomId);
        public async Task<List<Room>> Get(int hotelId, DateOnly from, DateOnly to, int people)
        {
            var rooms = await _db.Rooms.Include(r => r.Bookings)
                                       .Where(r => r.HotelId == hotelId && r.Capacity >= people)
                                       .ToListAsync();

            return [.. rooms.Where(r => !r.Bookings.Any(b => from < b.CheckOut && b.CheckIn < to))];
        }     

    }
}
