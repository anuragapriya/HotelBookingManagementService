using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using HotelBookingService.HotelBookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingService.HotelBookingService.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _db;
        public HotelRepository(AppDbContext db) { _db = db; }

        public async Task<List<Hotel>> Get()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var hotels = await _db.Hotels
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Bookings)
                .Select(h => new Hotel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Rooms = h.Rooms.Select(r => new Room
                    {
                        Id = r.Id,
                        HotelId = r.HotelId,
                        Number = r.Number,
                        Capacity = r.Capacity,
                        Type = r.Type,
                        Status = r.Bookings.Any(b => today >= b.CheckIn && today < b.CheckOut)
                                    ? RoomStatus.Booked
                                    : RoomStatus.Available,
                        Bookings = r.Bookings.Select(b => new Booking
                        {
                            Id = b.Id,
                            HotelId = b.HotelId,
                            RoomId = b.RoomId,
                            Reference = b.Reference,
                            GuestName = b.GuestName,
                            PartySize = b.PartySize,
                            CheckIn = b.CheckIn,
                            CheckOut = b.CheckOut
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return hotels;
        }

        public async Task<List<Hotel>> Get(string name)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var hotels = await _db.Hotels
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Bookings)
                .Where(h => EF.Functions.Like(h.Name, $"%{name}%"))
               .Select(h => new Hotel
               {
                   Id = h.Id,
                   Name = h.Name,
                   Rooms = h.Rooms.Select(r => new Room
                   {
                       Id = r.Id,
                       HotelId = r.HotelId,
                       Number = r.Number,
                       Capacity = r.Capacity,
                       Type = r.Type,
                       Status = r.Bookings.Any(b => today >= b.CheckIn && today < b.CheckOut)
                                    ? RoomStatus.Booked
                                    : RoomStatus.Available,
                       Bookings = r.Bookings.Select(b => new Booking
                       {
                           Id = b.Id,
                           HotelId = b.HotelId,
                           RoomId = b.RoomId,
                           Reference = b.Reference,
                           GuestName = b.GuestName,
                           PartySize = b.PartySize,
                           CheckIn = b.CheckIn,
                           CheckOut = b.CheckOut
                       }).ToList()
                   }).ToList()
               })
                .ToListAsync();

            return hotels;
        }

        public async Task<Hotel?> Get(int id)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var hotel = await _db.Hotels
     .Include(h => h.Rooms)
         .ThenInclude(r => r.Bookings)
     .Where(h => h.Id == id)
         .Select(h => new Hotel
         {
             Id = h.Id,
             Name = h.Name,
             Rooms = h.Rooms.Select(r => new Room
             {
                 Id = r.Id,
                 HotelId = r.HotelId,
                 Number = r.Number,
                 Capacity = r.Capacity,
                 Type = r.Type,
                 Status = r.Bookings.Any(b => today >= b.CheckIn && today < b.CheckOut)
                                    ? RoomStatus.Booked
                                    : RoomStatus.Available,
                 Bookings = r.Bookings.Select(b => new Booking
                 {
                     Id = b.Id,
                     HotelId = b.HotelId,
                     RoomId = b.RoomId,
                     Reference = b.Reference,
                     GuestName = b.GuestName,
                     PartySize = b.PartySize,
                     CheckIn = b.CheckIn,
                     CheckOut = b.CheckOut
                 }).ToList()
             }).ToList()
         })
         .FirstOrDefaultAsync();

            return hotel;
        }

    }
}