using AutoMapper;
using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Interfaces;

namespace HotelBookingService.HotelBookingService.Application.Services
{
    public class RoomService :IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        public RoomService(IMapper mapper, IRoomRepository roomRepo)
        {
            _mapper = mapper;
            _roomRepository = roomRepo;
        }
        public async Task<Result<List<RoomResponseDto>>> Get(int hotelId)
        {
            var rooms = await _roomRepository.Get(hotelId);
            var mappedRooms = _mapper.Map<List<RoomResponseDto>>(rooms);
            return Result<List<RoomResponseDto>>.Ok(mappedRooms);
        }
        public async Task<Result<List<RoomResponseDto>>> Get(int hotelId, DateOnly from, DateOnly to, int people)
        {
            if (from >= to) Result<List<RoomResponseDto>>.Fail("Check-in must be before check-out");
            var availableRooms = await _roomRepository.Get(hotelId, from, to, people);
            var mappedRooms = _mapper.Map<List<RoomResponseDto>>(availableRooms);
            return Result<List<RoomResponseDto>>.Ok(mappedRooms);
        }
    }
}
