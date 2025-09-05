using AutoMapper;
using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Application.Services;
using HotelBookingService.HotelBookingService.Domain.Entities;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace HotelBookingService.HotelBookingService.Tests.Services
{
    [TestFixture]
    public class HotelServiceTests
    {
        private Mock<IMapper> _mapperMock = null!;
        private Mock<IHotelRepository> _hotelRepoMock = null!;
        private HotelService _hotelService = null!;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _hotelRepoMock = new Mock<IHotelRepository>();
            _hotelService = new HotelService(_mapperMock.Object, _hotelRepoMock.Object);
        }

        [Test]
        public async Task Get_ShouldReturnHotelList_WhenHotelsExist()
        {
            // Arrange
            var hotels = new List<Hotel> { new() { Id = 1, Name = "Test Hotel" } };
            var hotelDtos = new List<HotelResponseDto> { new() { Id = 1, Name = "Test Hotel" } };

            _hotelRepoMock.Setup(r => r.Get()).ReturnsAsync(hotels);
            _mapperMock.Setup(m => m.Map<List<HotelResponseDto>>(hotels)).Returns(hotelDtos);

            // Act
            var result = await _hotelService.Get();

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result?.Data?.Count, Is.EqualTo(1));
            Assert.That(result?.Data[0]?.Name, Is.EqualTo("Test Hotel"));
        }

        [Test]
        public async Task Get_ById_ShouldReturnHotel_WhenHotelExists()
        {
            // Arrange
            var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
            var hotelDto = new HotelResponseDto { Id = 1, Name = "Test Hotel" };

            _hotelRepoMock.Setup(r => r.Get(1)).ReturnsAsync(hotel);
            _mapperMock.Setup(m => m.Map<HotelResponseDto>(hotel)).Returns(hotelDto);

            // Act
            var result = await _hotelService.Get(1);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Id, Is.EqualTo(1));
            Assert.That(result.Data.Name, Is.EqualTo("Test Hotel"));
        }

        [Test]
        public async Task Get_ById_ShouldFail_WhenHotelDoesNotExist()
        {
            // Arrange
            _hotelRepoMock.Setup(r => r.Get(99)).ReturnsAsync((Hotel?)null);

            // Act
            var result = await _hotelService.Get(99);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Hotel not found.")); // make sure this matches your Result<T>
        }

        [Test]
        public async Task Get_ByName_ShouldReturnHotels_WhenHotelsExist()
        {
            // Arrange
            var hotels = new List<Hotel> { new() { Id = 2, Name = "Luxury Inn" } };
            var hotelDtos = new List<HotelResponseDto> { new() { Id = 2, Name = "Luxury Inn" } };

            _hotelRepoMock.Setup(r => r.Get("Luxury")).ReturnsAsync(hotels);
            _mapperMock.Setup(m => m.Map<List<HotelResponseDto>>(hotels)).Returns(hotelDtos);

            // Act
            var result = await _hotelService.Get("Luxury");

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0].Name, Is.EqualTo("Luxury Inn"));
        }

        [Test]
        public async Task Get_ByName_ShouldReturnEmpty_WhenHotelsDoNotExist()
        {
            // Arrange
            _hotelRepoMock.Setup(r => r.Get("Unknown")).ReturnsAsync(new List<Hotel>());
            _mapperMock.Setup(m => m.Map<List<HotelResponseDto>>(It.IsAny<List<Hotel>>()))
                       .Returns(new List<HotelResponseDto>());

            // Act
            var result = await _hotelService.Get("Unknown");

            // Assert
            Assert.That(result.Success, Is.False); 
            Assert.That(result.Data, Is.Null);
        }
    }
}
