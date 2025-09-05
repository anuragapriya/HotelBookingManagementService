using AutoMapper;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Application.Services;
using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingService.HotelBookingService.Tests.Services
{
    [TestFixture]
    public class BookingServiceTests
    {
        private Mock<IMapper> _mapperMock = null!;
        private Mock<IBookingRepository> _bookingRepoMock = null!;
        private Mock<IRoomRepository> _roomRepoMock = null!;
        private BookingService _bookingService = null!;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _bookingRepoMock = new Mock<IBookingRepository>();
            _roomRepoMock = new Mock<IRoomRepository>();
            _bookingService = new BookingService(_mapperMock.Object, _bookingRepoMock.Object, _roomRepoMock.Object);
        }

        [Test]
        public async Task BookRoom_ShouldSucceed_WhenRoomAvailableAndCapacityOk()
        {
            // Arrange
            var request = new BookingRequestDto
            {
                HotelId = 1,
                RoomId = 1,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                PartySize = 2
            };
            var room = new Room { Id = 1, HotelId = 1, Capacity = 3, Number = "201" };
            var booking = new Booking
            {
                Id = 1,
                HotelId = 1,
                RoomId = 1,
                Reference = "REF123",
                GuestName = "John Doe",
                PartySize = 2,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                CreatedUtc = DateTime.UtcNow
            };
            var responseDto = new BookingResponseDto
            {
                Id = 1,
                HotelId = 1,
                RoomId = 1,
                Reference = "REF123",
                GuestName = "John Doe",
                PartySize = 2,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                CreatedUtc = DateTime.UtcNow
            };

            _bookingRepoMock.Setup(r => r.IsRoomAvailable(request.HotelId, request.RoomId, request.CheckIn, request.CheckOut))
                .ReturnsAsync(true);
            _roomRepoMock.Setup(r => r.Get(request.HotelId, request.RoomId)).ReturnsAsync(room);
            _mapperMock.Setup(m => m.Map<Booking>(request)).Returns(booking);
            _mapperMock.Setup(m => m.Map<BookingResponseDto>(booking)).Returns(responseDto);

            // Act
            var result = await _bookingService.BookRoom(request);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result?.Data?.Id, Is.EqualTo(1));
            _bookingRepoMock.Verify(r => r.AddAsync(booking), Times.Once);
            _bookingRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task BookRoom_ShouldFail_WhenRoomNotAvailable()
        {
            var request = new BookingRequestDto
            {
                HotelId = 1,
                RoomId = 1,   
                GuestName = "John Doe",
                PartySize = 2,
                CheckIn = DateOnly.FromDateTime(DateTime.Today),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1))              
            };
            
            _bookingRepoMock.Setup(r => r.IsRoomAvailable(request.HotelId, request.RoomId, request.CheckIn, request.CheckOut))
                .ReturnsAsync(false);

            var result = await _bookingService.BookRoom(request);

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Room not available for the selected dates"));
        }

        [Test]
        public async Task BookRoom_ShouldFail_WhenPartySizeExceedsCapacity()
        {
            var request = new BookingRequestDto
            {
                HotelId = 1,
                RoomId = 1,   
                GuestName = "John Doe",
                PartySize = 5,
                CheckIn = DateOnly.FromDateTime(DateTime.Today),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1))              
            };
            var room = new Room { Id = 1, HotelId = 1, Capacity = 3, Number = "201" };

            _bookingRepoMock.Setup(r => r.IsRoomAvailable(request.HotelId, request.RoomId, request.CheckIn, request.CheckOut))
                .ReturnsAsync(true);
            _roomRepoMock.Setup(r => r.Get(request.HotelId, request.RoomId)).ReturnsAsync(room);

            var result = await _bookingService.BookRoom(request);

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Room capacity is 3, but you requested 5"));
        }

        [Test]
        public async Task BookRoom_ShouldFail_WhenCheckOutBeforeCheckIn()
        {
            var request = new BookingRequestDto
            {
                HotelId = 1,
                RoomId = 1,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                PartySize = 2
            };

            var result = await _bookingService.BookRoom(request);

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Check-out date must be after check-in date"));
        }

        [Test]
        public async Task Get_ShouldReturnBooking_WhenBookingExists()
        {
            var booking = new Booking
            {
                Id = 1,
                HotelId = 1,
                RoomId = 1,
                Reference = "REF123",
                GuestName = "John Doe",
                PartySize = 2,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                CreatedUtc = DateTime.UtcNow
            };
            var responseDto = new BookingResponseDto
            {
                Id = 1,
                HotelId = 1,
                RoomId = 1,
                Reference = "REF123",
                GuestName = "John Doe",
                PartySize = 2,
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                CreatedUtc = DateTime.UtcNow
            };

            _bookingRepoMock.Setup(r => r.Get("REF123")).ReturnsAsync(booking);
            _mapperMock.Setup(m => m.Map<BookingResponseDto>(booking)).Returns(responseDto);

            var result = await _bookingService.Get("REF123");

            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result?.Data?.Reference, Is.EqualTo("REF123"));
        }
    }
}
