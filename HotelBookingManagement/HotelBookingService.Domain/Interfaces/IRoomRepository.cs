using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> Get(int hotelId);
        Task<Room?> Get(int hotelId, int roomId);
        Task<List<Room>> Get(int hotelId, DateOnly from, DateOnly to, int people);        
    }
}
