using AutoMapper;
using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using HotelBookingService.HotelBookingService.Infrastructure.Repositories;

namespace HotelBookingService.HotelBookingService.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IMapper _mapper;
        private readonly IHotelRepository _hotelRepository;
        public HotelService(IMapper mapper, IHotelRepository hotelRepository)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task<Result<List<HotelResponseDto>>> Get()
        {
            var hotels = await _hotelRepository.Get();
            var hotelDtos = _mapper.Map<List<HotelResponseDto>>(hotels);
            return Result<List<HotelResponseDto>>.Ok(hotelDtos);
        }

        public async Task<Result<HotelResponseDto>> Get(int hotelId)
        {
            var hotel = await _hotelRepository.Get(hotelId);
            if (hotel == null)
            {
                return Result<HotelResponseDto>.Fail("Hotel not found.");
            }
            var hotelDto = _mapper.Map<HotelResponseDto>(hotel);
            return Result<HotelResponseDto>.Ok(hotelDto);
        }

        public async Task<Result<List<HotelResponseDto>>> Get(string name)
        {
            var hotels = await _hotelRepository.Get(name);
            if (hotels == null || hotels.Count == 0)
            {
                return Result<List<HotelResponseDto>>.Fail("Hotels not found.");
            }
            var hotelDtos = _mapper.Map<List<HotelResponseDto>>(hotels);
            return Result<List<HotelResponseDto>>.Ok(hotelDtos);
        }
    }
}
