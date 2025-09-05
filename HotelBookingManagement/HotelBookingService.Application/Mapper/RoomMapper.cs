using AutoMapper;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Entities;

namespace HotelBookingService.HotelBookingService.Application.Mapper
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateMap<Room, RoomResponseDto>().ReverseMap();
        }
    }
}
