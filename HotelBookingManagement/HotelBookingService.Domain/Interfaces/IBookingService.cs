using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;

namespace HotelBookingService.HotelBookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        Task<Result<BookingResponseDto>> Get(string reference);
        Task<Result<BookingResponseDto>> BookRoom(BookingRequestDto request);
    }
}
