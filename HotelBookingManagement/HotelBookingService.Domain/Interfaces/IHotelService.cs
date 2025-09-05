using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IHotelService
    {
        Task<Result<List<HotelResponseDto>>> Get();
        Task<Result<HotelResponseDto>> Get(int id);
        Task<Result<List<HotelResponseDto>>> Get(string name);
    }
}
