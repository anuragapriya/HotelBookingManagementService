using AutoMapper;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Application.Mapper
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingRequestDto, Booking>().ReverseMap();
            CreateMap<Booking,BookingResponseDto>().ReverseMap();
        }
    }
}
