using AutoMapper;
using HotelBookingService.HotelBookingService.Application.Common;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingService.HotelBookingService.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;

        public BookingService(IMapper mapper,IBookingRepository bookingRepo,IRoomRepository roomRepo)
        {
            _mapper = mapper;
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
        }
        
        public async Task<Result<BookingResponseDto>> BookRoom(BookingRequestDto request)
        {
            if (request.CheckIn >= request.CheckOut)
                return Result<BookingResponseDto>.Fail("Check-out date must be after check-in date");
            var available = await _bookingRepo.IsRoomAvailable(request.HotelId, request.RoomId, request.CheckIn, request.CheckOut);

            if (!available) return Result<BookingResponseDto>.Fail("Room not available for the selected dates");
            var room = await _roomRepo.Get(request.HotelId, request.RoomId);
            if (room != null && request.PartySize > room.Capacity)
            {               
                return Result<BookingResponseDto>.Fail($"Room capacity is {room?.Capacity}, but you requested {request.PartySize}");
            }

            var booking = _mapper.Map<Booking>(request);
            booking.Reference = GenerateBookingReference();

            await _bookingRepo.AddAsync(booking);
            await _bookingRepo.SaveChangesAsync();
            var response = _mapper.Map<BookingResponseDto>(booking);
            return Result<BookingResponseDto>.Ok(response);
        }

        private static string GenerateBookingReference()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }

        public async Task<Result<BookingResponseDto>> Get(string reference)
        {
            var bookingResponse= await _bookingRepo.Get(reference);
            return Result<BookingResponseDto>.Ok( _mapper.Map<BookingResponseDto>(bookingResponse));
        }
    }
}
