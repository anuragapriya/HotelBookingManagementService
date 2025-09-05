using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingService.HotelBookingService.Api.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset()
        {
            _db.Bookings.RemoveRange(_db.Bookings);
            _db.Rooms.RemoveRange(_db.Rooms);
            _db.Hotels.RemoveRange(_db.Hotels);
            await _db.SaveChangesAsync();

            await _db.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Bookings';");
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Rooms';");
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Hotels';");

            return Ok();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            if (!_db.Hotels.Any())
            {
                var hotels = new List<Hotel>
                                    {
                                        new () { Name = "Grand Palace Hotel" },
                                        new () { Name = "Ocean View Resort" },
                                        new () { Name = "Mountain Retreat" },
                                        new () { Name = "City Center Inn" },
                                        new () { Name = "Sunset Boutique Hotel" }
                                    };

                foreach (var hotel in hotels)
                {
                    var rooms = new List<Room>
                                    {
                                        // Single rooms
                                        new() { Number="101", Type=RoomType.Single, Capacity=1, Hotel=hotel },
                                        new() { Number="102", Type=RoomType.Single, Capacity=1, Hotel=hotel },
                                        new() { Number="103", Type=RoomType.Single, Capacity=1, Hotel=hotel },

                                        // Double rooms
                                        new() { Number="201", Type=RoomType.Double, Capacity=2, Hotel=hotel },
                                        new() { Number="202", Type=RoomType.Double, Capacity=2, Hotel=hotel },
                                        new() { Number="203", Type=RoomType.Double, Capacity=2, Hotel=hotel },

                                        // Deluxe rooms
                                        new() { Number="301", Type=RoomType.Deluxe, Capacity=4, Hotel=hotel },
                                        new() { Number="302", Type=RoomType.Deluxe, Capacity=4, Hotel=hotel },
                                        new() { Number="303", Type=RoomType.Deluxe, Capacity=4, Hotel=hotel },

                                    };

                    hotel.Rooms.AddRange(rooms);
                    _db.Hotels.Add(hotel);
                }

                await _db.SaveChangesAsync();
            }

            return Ok();
        }

    }
}