using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> Get();
        Task<Hotel?> Get(int id);
        Task<List<Hotel>> Get(string name);
    }
}
