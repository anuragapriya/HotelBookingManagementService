using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IRoomService
    {
        Task<Result<List<RoomResponseDto>>> Get(int hotelId, DateOnly from, DateOnly to, int people);
        Task<Result<List<RoomResponseDto>>> Get(int hotelId);
    }
}
